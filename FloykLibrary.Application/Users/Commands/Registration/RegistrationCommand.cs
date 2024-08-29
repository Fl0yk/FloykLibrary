using MediatR;

namespace FloykLibrary.Application.Users.Commands.Registration
{
    public class RegistrationCommand : IRequest<RegistrationCommandResponse>
    {
        public required string Name { get; init; }

        public required string Email { get; init; }
    }
}
