using FloykLibrary.Infrastructure.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FloykLibrary.Infrastructure.Data.Seeders
{
    public static class AuthorBookSeeder
    {
        public static void SeedAuthorBook(this EntityTypeBuilder<AuthorBook> auConfigBuilder)
        {
            auConfigBuilder.HasData([
                new AuthorBook() 
                {
                    AuthorId = Guid.Parse("35F010ED-8C38-4EEB-B9EC-5FB56CCF3189"),
                    BookId = Guid.Parse("25F010ED-8C38-4EEB-B9EC-5FB56CCF3189"),
                },
                new AuthorBook()
                {
                    AuthorId = Guid.Parse("35F010ED-8C38-4EEB-B9EC-5FB56CCF3189"),
                    BookId = Guid.Parse("24F010ED-8C38-4EEB-B9EC-5FB56CCF3189"),
                },

                new AuthorBook()
                {
                    AuthorId = Guid.Parse("34F010ED-8C38-4EEB-B9EC-5FB56CCF3189"),
                    BookId = Guid.Parse("23F010ED-8C38-4EEB-B9EC-5FB56CCF3189"),
                },

                new AuthorBook()
                {
                    AuthorId = Guid.Parse("33F010ED-8C38-4EEB-B9EC-5FB56CCF3189"),
                    BookId = Guid.Parse("23F010ED-8C38-4EEB-B9EC-5FB56CCF3189"),
                },
                ]);
        }
    }
}
