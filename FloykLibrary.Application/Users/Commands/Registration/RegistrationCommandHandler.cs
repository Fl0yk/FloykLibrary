using AutoMapper;
using FloykLibrary.Application.Shared.Abstractions;
using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;
using MediatR;

namespace FloykLibrary.Application.Users.Commands.Registration
{
    public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, RegistrationCommandResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider;
        private readonly IMapper _mapper;

        public RegistrationCommandHandler(IUnitOfWork unitOfWork, 
                                            IJwtProvider jwtProvider,
                                            IMapper mapper)
        {
            _userRepository = unitOfWork.UserRepository;
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
            _mapper = mapper;
        }

        public async Task<RegistrationCommandResponse> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            User user = _mapper.Map<User>(request);

            user.Roles.Add(Role.Client);
            await _userRepository.CreateAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            string jwt = _jwtProvider.GenerateJwt(user);
            string refresh = _jwtProvider.GenerateRefreshToken();

            user.RefreshToken = refresh;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(1);

            await _userRepository.UpdateAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new() { JwtToken = jwt, RefreshToken = refresh };
        }
    }
}
