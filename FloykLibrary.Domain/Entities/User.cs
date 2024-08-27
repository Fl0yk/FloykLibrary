namespace FloykLibrary.Domain.Entities
{
    public class User : Entity
    {
        public required string Name { get; set; }

        public required string Email { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiry { get; set; }

        public List<Role> Roles { get; set; } = [];

        public List<Book> TakenBooks { get; set; } = [];
    }
}
