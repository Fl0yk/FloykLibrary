namespace FloykLibrary.Application.Shared.Models.DTOs
{
    public class BookDTO
    {
        public required Guid Id { get; init; }

        public required string ISBN { get; init; }

        public required string Title { get; init; }

        public required string Description { get; init; }

        public required string Genre { get; init; }

        public List<InnerAuthorDTO> Authors { get; init; } = [];

        public DateTime? TakingBook { get; init; }

        public DateTime? ReturningBook { get; init; }

        public string? Image { get; init; }
    }

    public class InnerAuthorDTO
    {
        public required Guid Id { get; init; }

        public required string Name { get; init; }

        public required string Surname { get; init; }

        public required DateTime DateOfBirth { get; init; }

        public required string Country { get; init; }
    }

}
