using MediatR;

namespace FloykLibrary.Application.Authors.Commands.CreateAuthor
{
    public record class CreateAuthorCommand : IRequest<Guid>
    {
        public required string Name { get; init; }

        public required string Surname { get; init; }

        public required DateTime DateOfBirth { get; init; }

        public required string Country { get; init; }
    }
}
