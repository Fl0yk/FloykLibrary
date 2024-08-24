namespace FloykLibrary.Application.Shared.Models.DTOs
{
    public class BookDTO
    {
        public required Guid Id { get; set; }

        public required string ISBN { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public required string Genre { get; set; }

        public List<InnerAuthorDTO> Authors { get; set; } = [];

        public DateTime? TakingBook { get; set; }

        public DateTime? ReturningBook { get; set; }

        public string? Image { get; set; }
    }

    public class InnerAuthorDTO
    {
        public required Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Surname { get; set; }
    }

}
