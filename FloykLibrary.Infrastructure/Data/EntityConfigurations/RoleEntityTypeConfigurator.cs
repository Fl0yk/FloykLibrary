using FloykLibrary.Domain.Entities;
using FloykLibrary.Infrastructure.Data.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FloykLibrary.Infrastructure.Data.EntityConfigurations
{
    internal class RoleEntityTypeConfigurator : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();

            builder.SeedRoles();
        }
    }
}
