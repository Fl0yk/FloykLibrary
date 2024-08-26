using MediatR;

namespace FloykLibrary.Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand : IRequest<Guid>
    {
        public required Guid Id { get; init; }

        public required string Name { get; init; }

        public required string Surname { get; init; }

        public required DateTime DateOfBirth { get; init; }

        public required string Country { get; init; }
    }
}
