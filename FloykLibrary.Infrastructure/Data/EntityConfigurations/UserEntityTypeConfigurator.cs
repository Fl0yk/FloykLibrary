using FloykLibrary.Domain.Entities;
using FloykLibrary.Infrastructure.Data.Seeders;
using FloykLibrary.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FloykLibrary.Infrastructure.Data.EntityConfigurations
{
    internal class UserEntityTypeConfigurator : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasAlternateKey(x => x.Email);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Email).IsRequired();

            builder.HasMany(x => x.Roles)
                    .WithMany()
                    .UsingEntity<UserRole>(cnf => cnf.SeedUserRole());

            builder.HasMany(x => x.TakenBooks)
                    .WithMany();

            builder.SeedUsers();
        }
    }
}
