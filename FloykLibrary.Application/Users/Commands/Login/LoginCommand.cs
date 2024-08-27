using MediatR;

namespace FloykLibrary.Application.Users.Commands.Login
{
    public class LoginCommand : IRequest<LoginCommandResponse>
    {
        public required string Name { get; init; }

        public required string Email { get; init; }
    }
}
