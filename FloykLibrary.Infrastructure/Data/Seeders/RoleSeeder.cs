using FloykLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FloykLibrary.Infrastructure.Data.Seeders
{
    internal static class RoleSeeder
    {
        public static void SeedRoles(this EntityTypeBuilder<Role> builder)
        {
            builder.HasData([
                Role.Admin,
                Role.Client
                ]);
        }
    }
}
