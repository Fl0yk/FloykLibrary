using MediatR;

namespace FloykLibrary.Application.Books.Commands.UpdateBook
{
    public record class UpdateBookCommand : IRequest<Guid>
    {
        public required Guid Id { get; init; }

        public required string ISBN { get; init; }

        public required string Title { get; init; }

        public required string Description { get; init; }

        public required string Genre { get; init; }

        public List<UpdateBookInnerAuthor> Authors { get; init; } = [];
    }

    public class UpdateBookInnerAuthor
    {
        public required Guid Id { get; init; }

        public required string Name { get; init; }

        public required string Surname { get; init; }

        public required DateTime DateOfBirth { get; init; }

        public required string Country { get; init; }
    }
}
