using FloykLibrary.Domain.Entities;
using FloykLibrary.Infrastructure.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FloykLibrary.Infrastructure.Data.Seeders
{
    internal static class UserRoleSeeder
    {
        public static void SeedUserRole(this EntityTypeBuilder<UserRole> builder)
        {
            builder.HasData([
                new UserRole()
                {
                    RoleId = Role.Admin.Id,
                    UserId = Guid.Parse("11F010ED-8C38-4EEB-B9EC-5FB56CCF3189")
                },
                new UserRole()
                {
                    RoleId = Role.Client.Id,
                    UserId = Guid.Parse("11F010ED-8C38-4EEB-B9EC-5FB56CCF3189")
                },
                ]);
        }
    }
}
