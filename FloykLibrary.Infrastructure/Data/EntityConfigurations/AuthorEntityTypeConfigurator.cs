using FloykLibrary.Domain.Entities;
using FloykLibrary.Infrastructure.Data.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FloykLibrary.Infrastructure.Data.EntityConfigurations
{
    internal class AuthorEntityTypeConfigurator : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> authorConfigBuilder)
        {
            authorConfigBuilder.HasKey(a => a.Id);

            authorConfigBuilder.Property(a => a.Name).IsRequired().HasMaxLength(30);
            authorConfigBuilder.Property(a => a.Surname).IsRequired().HasMaxLength(30);
            authorConfigBuilder.Property(a => a.Country).IsRequired().HasMaxLength(30);
            authorConfigBuilder.Property(a => a.DateOfBirth).IsRequired();

            authorConfigBuilder.SeedAuthors();
        }
    }
}
