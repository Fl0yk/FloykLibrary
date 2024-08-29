using FloykLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FloykLibrary.Infrastructure.Data.Seeders
{
    internal static class UserSeeder
    {
        public static void SeedUsers(this EntityTypeBuilder<User> builder)
        {
            builder.HasData([
                new User()
                {
                    Id = Guid.Parse("11F010ED-8C38-4EEB-B9EC-5FB56CCF3189"),
                    Name = "Admin",
                    Email = "admin@mail.ru",
                }
                ]);
        }
    }
}
