using FloykLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FloykLibrary.Infrastructure.Data.Seeders
{
    public static class BookSeeder 
    {
        public static void SeedBooks(this EntityTypeBuilder<Book> bookConfBuilder)
        {
            bookConfBuilder.HasData([
                new Book()
                {
                    Id = Guid.Parse("25F010ED-8C38-4EEB-B9EC-5FB56CCF3189"),
                    ISBN = "5170064004",
                    Title = "Война и мир. Книга 1",
                    Description = "В книгу вошли первый и второй тома романа «Война и мир» – одного из самых знаменитых произведений литературы XIX века.",
                    Genre = "Роман"
                },
                new Book()
                {
                    Id = Guid.Parse("24F010ED-8C38-4EEB-B9EC-5FB56CCF3189"),
                    ISBN = "5170287402",
                    Title = "Мертвые души",
                    Description = "«Мертвые души» — гениальное произведение Николая Васильевича Гоголя, " +
                                    "учебник жизни и ключ к пониманию характеров и типажей нашего общества. " +
                                    "Сам автор определил жанр произведения как поэму.",
                    Genre = "Поэма"
                },
                new Book()
                {
                    Id = Guid.Parse("23F010ED-8C38-4EEB-B9EC-5FB56CCF3189"),
                    ISBN = "9785041772932",
                    Title = "Благие знамения",
                    Description = "Книга получила в целом положительные оценки критиков. Роман номинировался на Всемирную премию фэнтези и премию журнала «Локус».",
                    Genre = "Роман"
                },
                ]);
        }
    }
}
