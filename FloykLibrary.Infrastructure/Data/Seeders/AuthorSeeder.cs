using FloykLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FloykLibrary.Infrastructure.Data.Seeders
{
    public static class AuthorSeeder
    {
        public static void SeedAuthors(this EntityTypeBuilder<Author> authorConfBuilder)
        {
            authorConfBuilder.HasData([
                new Author()
                {
                    Id = Guid.Parse("35F010ED-8C38-4EEB-B9EC-5FB56CCF3189"),
                    Name = "Лев",
                    Surname = "Толстой",
                    DateOfBirth = DateTime.Parse("9.09.1828"),
                    Country = "Россия"
                },
                new Author()
                {
                    Id = Guid.Parse("34F010ED-8C38-4EEB-B9EC-5FB56CCF3189"),
                    Name = "Нил",
                    Surname = "Гейман",
                    DateOfBirth = DateTime.Parse("10.11.1960"),
                    Country = "Англия"
                },
                new Author()
                {
                    Id = Guid.Parse("33F010ED-8C38-4EEB-B9EC-5FB56CCF3189"),
                    Name = "Терри",
                    Surname = "Пратчетт",
                    DateOfBirth = DateTime.Parse("28.04.1948"),
                    Country = "Россия"
                }
                ]);
        }
    }
}
