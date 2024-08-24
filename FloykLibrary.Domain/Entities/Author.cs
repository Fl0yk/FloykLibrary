namespace FloykLibrary.Domain.Entities
{
    public class Author : Entity
    {
        public required string Name { get; set; }

        public required string Surname { get; set; }

        public required DateTime DateOfBirth { get; set; }

        public required string Country { get; set; }

        public List<Book> Books { get; set; } = [];
    }
}
