namespace FloykLibrary.Domain.Entities
{
    public class Book : Entity
    {
        public required string ISBN { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public required string Genre { get; set; }

        public List<Author> Authors { get; set; } = [];

        public Guid? UserId { get; set; }

        public User? User { get; set; }

        public DateTime? TakingBook { get; set; }

        public DateTime? ReturningBook { get; set; }

        public string? Image { get; set; }

        public bool IsTaken => UserId is not null;
    }
}
