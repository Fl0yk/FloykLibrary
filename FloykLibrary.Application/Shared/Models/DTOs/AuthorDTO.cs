namespace FloykLibrary.Application.Shared.Models.DTOs
{
    public class AuthorDTO
    {
        public required Guid Id { get; init; }

        public required string Name { get; init; }

        public required string Surname { get; init; }

        public required DateTime DateOfBirth { get; init; }

        public required string Country { get; init; }

        public List<InnerBookDTO> Books { get; init; } = [];
    }

    public class InnerBookDTO
    {
        public required Guid Id { get; init; }

        public required string ISBN { get;  init; }

        public required string Title { get; init; }

        public required string Description { get; init; }

        public required string Genre { get; init; }

        public DateTime? TakingBook { get; init; }

        public DateTime? ReturningBook { get; init; }

        public string? Image { get; init; }
    }

}
