namespace FloykLibrary.Application.Shared.Models.DTOs
{
    public class UserDTO
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }

        public required string Email { get; init; }

        public List<InnerRoleDTO> Roles { get; init; } = [];

        public List<BookDTO> TakenBooks { get; init; } = [];
    }

    public class InnerRoleDTO
    {
        public required int Id { get; init; }

        public required string Name { get; init; }
    }
}
