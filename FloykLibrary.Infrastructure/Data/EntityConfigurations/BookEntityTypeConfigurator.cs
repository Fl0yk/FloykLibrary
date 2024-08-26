using FloykLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FloykLibrary.Infrastructure.Data.EntityConfigurations
{
    internal class BookEntityTypeConfigurator : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> bookConfigBuilder)
        {
            bookConfigBuilder.HasKey(b => b.Id);

            bookConfigBuilder.Property(b => b.Title).IsRequired().HasMaxLength(50);
            bookConfigBuilder.Property(b => b.ISBN).IsRequired();
            bookConfigBuilder.Property(b => b.Description).IsRequired().HasMaxLength(250);

            bookConfigBuilder.HasMany(b => b.Authors).WithMany(a => a.Books);
        }
    }
}
