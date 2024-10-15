using FloykLibrary.Application.Shared.Abstractions;
using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;
using MediatR;

namespace FloykLibrary.Application.Users.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider;

        public LoginCommandHandler(IUnitOfWork unitOfWork, 
                                    IJwtProvider jwtProvider)
        {
            _userRepository = unitOfWork.UserRepository;
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User? dbUser = await _userRepository.FirstOrDefaultAsync(u => u.Name == request.Name && u.Email == request.Email, cancellationToken, u => u.Roles);

            if (dbUser is null)
                return new();

            string jwtToken = _jwtProvider.GenerateJwt(dbUser);
            string refreshToken = _jwtProvider.GenerateRefreshToken();

            dbUser.RefreshToken = refreshToken;
            dbUser.RefreshTokenExpiry = DateTime.UtcNow.AddDays(1);

            await _userRepository.UpdateAsync(dbUser, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new()
            {
                IsLogedIn = true,
                JwtToken = jwtToken,
                RefreshToken = refreshToken
            };
        }
    }
}
