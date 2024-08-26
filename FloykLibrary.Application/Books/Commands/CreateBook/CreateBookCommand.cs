using MediatR;

namespace FloykLibrary.Application.Books.Commands.CreateBook
{
    public record class CreateBookCommand : IRequest<Guid>
    {
        public required string ISBN { get; init; }

        public required string Title { get; init; }

        public required string Description { get; init; }

        public required string Genre { get; init; }

        public List<Guid> AuthorsId { get; init; } = [];
    }
}
