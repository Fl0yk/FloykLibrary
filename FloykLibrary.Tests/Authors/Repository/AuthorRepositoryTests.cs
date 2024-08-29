using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;
using FloykLibrary.Infrastructure;
using FloykLibrary.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FloykLibrary.Tests.Authors.Repository
{
    public class AuthorRepositoryTests
    {
        private readonly ApplicationDbContext _context;

        public AuthorRepositoryTests()
        {
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            _context = new ApplicationDbContext(dbOptions.Options);
        }

        [Fact]
        public async Task AddAuthor()
        {
            //Arrange
            IAuthorRepository repo = new AuthorRepository(_context);
            Author author = new()
            {
                Id = Guid.Parse("8C0D74A3-3EC1-4044-9098-A597A7910A80"),
                Name = "Test 1",
                Surname = "Test 1",
                Country = "Test",
                DateOfBirth = DateTime.Parse("2004-10-10")
            };

            //Act
            Guid id = await repo.CreateAsync(author);
            await _context.SaveChangesAsync();

            //Assert
            Assert.Equal(id, author.Id);
        }

        [Fact]
        public async Task GetUsers()
        {
            //Arrange
            Author[] authors = [
                new()
                {
                    Id = Guid.Parse("9C0D74A3-3EC1-4044-9098-A597A7910A80"),
                    Name = "Test 1",
                    Surname = "Test 1",
                    Country = "Test",
                    DateOfBirth = DateTime.Parse("2004-10-10")
                },
                new()
                {
                    Id = Guid.Parse("8C0D74A3-3EC1-4044-9098-A597A7910A80"),
                    Name = "Test 2",
                    Surname = "Test 2",
                    Country = "Test",
                    DateOfBirth = DateTime.Parse("2004-10-10")
                }];

            await _context.Authors.AddRangeAsync(authors);
            await _context.SaveChangesAsync();

            IAuthorRepository repo = new AuthorRepository(_context);

            //Act
            IQueryable<Author> dbAuthors = await repo.GetAllAsync(null);

            //Assert
            Assert.NotEmpty(dbAuthors);
            Assert.Equal(dbAuthors.Count(), authors.Length);
        }

        [Fact]
        public async Task GetAuthorById()
        {
            //Arrange
            Author author = new()
            {
                Id = Guid.Parse("8C0D74A3-3EC1-4044-9098-A597A7910A80"),
                Name = "Test 1",
                Surname = "Test 1",
                Country = "Test",
                DateOfBirth = DateTime.Parse("2004-10-10")
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            IAuthorRepository repo = new AuthorRepository(_context);

            //Act
            Author? dbAuthor = await repo.FirstOrDefaultAsync(u => u.Id == author.Id);

            //Assert
            Assert.NotNull(dbAuthor);
            Assert.Equal(dbAuthor.Name, author.Name);
            Assert.Equal(dbAuthor.Id, author.Id);
        }

        [Fact]
        public async Task DeleteAuthor()
        {
            //Arrange
            Author author = new()
            {
                Id = Guid.Parse("8C0D74A3-3EC1-4044-9098-A597A7910A80"),
                Name = "Test 1",
                Surname = "Test 1",
                Country = "Test",
                DateOfBirth = DateTime.Parse("2004-10-10")
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            IAuthorRepository repo = new AuthorRepository(_context);

            //Act
            await repo.DeleteAsync(author);
            await _context.SaveChangesAsync();

            //Assert
            Author? dbAuthor = await repo.FirstOrDefaultAsync(u => u.Id == author.Id);
            Assert.Null(dbAuthor);
        }

        [Fact]
        public async Task UpdateAuthor()
        {
            //Arrange
            Author author = new()
            {
                Id = Guid.Parse("8C0D74A3-3EC1-4044-9098-A597A7910A80"),
                Name = "Test 1",
                Surname = "Test 1",
                Country = "Test",
                DateOfBirth = DateTime.Parse("2004-10-10")
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            IAuthorRepository repo = new AuthorRepository(_context);

            //Act
            author.Name = "New name";
            Guid id = await repo.UpdateAsync(author);
            await _context.SaveChangesAsync();

            //Assert
            Author? dbAuthor = await repo.FirstOrDefaultAsync(u => u.Id == author.Id);
            Assert.NotNull(dbAuthor);
            Assert.Equal(author.Name, dbAuthor.Name);
            Assert.Equal(author.Id, dbAuthor.Id);
        }
    }
}
