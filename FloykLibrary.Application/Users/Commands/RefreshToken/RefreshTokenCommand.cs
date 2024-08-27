using MediatR;

namespace FloykLibrary.Application.Users.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<RefreshTokenCommandResponse>
    {
        public required string Jwt {  get; set; }

        public required string RefreshToken { get; set; }
    }
}
