using FloykLibrary.Application.Shared.Abstractions;
using FloykLibrary.Application.Shared.Exceptions;
using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;
using MediatR;
using System.Security.Claims;

namespace FloykLibrary.Application.Users.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler 
        : IRequestHandler<RefreshTokenCommand, RefreshTokenCommandResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider;

        public RefreshTokenCommandHandler(IUnitOfWork unitOfWork, 
                                            IJwtProvider jwtProvider)
        {
            _jwtProvider = jwtProvider;
            _userRepository = unitOfWork.UserRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            ClaimsPrincipal? principal = _jwtProvider.GetClaimsPrincipal(request.Jwt);

            Claim? id = principal?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (id is null || !Guid.TryParse(id.Value, out Guid guidId))
            {
                throw new InvalidTokenException("Invalid data in token");
            }

            User? dbUser = await _userRepository.FirstOrDefaultAsync(u => u.Id == guidId, cancellationToken, u => u.Roles);

            if (dbUser is null)
                throw new KeyNotFoundException("Invalid User from token");

            if (dbUser.RefreshToken is null 
                    || dbUser.RefreshToken != request.RefreshToken 
                    || dbUser.RefreshTokenExpiry < DateTime.UtcNow)
            {
                dbUser.RefreshToken = null;
                dbUser.RefreshTokenExpiry = null;

                await _userRepository.UpdateAsync(dbUser, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                throw new InvalidTokenException("Refresh token is not valid");
            }

            string jwt = _jwtProvider.GenerateJwt(dbUser);

            return new() { Jwt = jwt, RefreshToken = dbUser.RefreshToken! };
        }
    }
}
