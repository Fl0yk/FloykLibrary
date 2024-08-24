namespace FloykLibrary.Domain.Entities
{
    public class User : Entity
    {
        public required string Name { get; set; }

        public List<Book> TakenBooks { get; set; } = [];
    }
}
