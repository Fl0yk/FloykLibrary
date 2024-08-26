using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FloykLibrary.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly DbSet<Book> _books;
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        { 
            _books = context.Books;
            _context = context;
        }

        public Task AddImageAsync(Guid guidId, string image, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateAsync(Book entity, CancellationToken token = default)
        {
            if (entity.Authors.Count > 0)
            {
                _context.Authors.AttachRange(entity.Authors);
            }

            _books.Add(entity);

            return Task.FromResult(entity.Id);
        }

        public Task DeleteAsync(Book entity, CancellationToken token = default)
        {
            _books.Remove(entity);

            return Task.CompletedTask;
        }

        public Task<Book?> FirstOrDefaultAsync(Expression<Func<Book, bool>> filtres, 
                                                CancellationToken token = default, 
                                                params Expression<Func<Book, object>>[]? includesProperties)
        {
            return _books.AsNoTracking().FirstOrDefaultAsync(filtres, token);
        }

        public Task<IQueryable<Book>> GetAllAsync(Expression<Func<Book, bool>>? filtres, 
                                                    CancellationToken token = default, 
                                                    params Expression<Func<Book, object>>[]? includesProperties)
        {
            if (filtres is null)
                return Task.FromResult(_books.AsNoTracking());

            return Task.FromResult(_books.AsNoTracking().Where(filtres));
        }

        public async Task<bool> IsIsbnUniqueAsync(string isbn, CancellationToken token = default)
        {
            return !await _books.AnyAsync(b => b.ISBN  == isbn, token);
        }

        public Task<Guid> UpdateAsync(Book entity, CancellationToken token = default)
        {
            if (entity.Authors.Count > 0)
            {
                _context.Authors.AttachRange(entity.Authors);
            }

            _books.Update(entity);

            return Task.FromResult(entity.Id);
        }
    }
}
