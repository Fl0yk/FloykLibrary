namespace FloykLibrary.Domain.Entities
{
    public class Role
    {
        public static Role Client => new(1, "client");
        public static Role Admin => new(2, "admin");

        public int Id { get; init; }

        public string Name { get; init; } = string.Empty;

        protected Role(int id, string name)
        {
            Id = id;
            Name = name.ToLower();
        }

        public override string ToString() => Name;

        public static explicit operator int(Role role) => role.Id;

        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;

            if (GetType() != obj.GetType()) 
                return false;

            return obj is Role role && role.Id.Equals(Id);
        }

        public override int GetHashCode() => Id.GetHashCode() * 28;
    }
}
