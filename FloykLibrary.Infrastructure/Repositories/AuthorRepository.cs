using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FloykLibrary.Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DbSet<Author> _authors;
        private readonly ApplicationDbContext _context;

        public AuthorRepository(ApplicationDbContext context)
        {
            _authors = context.Authors;
            _context = context;
        }

        public Task<Guid> CreateAsync(Author entity, CancellationToken token = default)
        {
            if (entity.Books.Count != 0)
            {
                _context.Books.AttachRange(entity.Books);
            }

            _authors.Add(entity);

            return Task.FromResult(entity.Id);
        }

        public Task DeleteAsync(Author entity, CancellationToken token = default)
        {
            _authors.Remove(entity);

            return Task.CompletedTask;
        }

        public async Task<Author?> FirstOrDefaultAsync(Expression<Func<Author, bool>> filtres,
                                                    CancellationToken token = default,
                                                    params Expression<Func<Author, object>>[]? includesProperties)
        {
            return await _authors.AsNoTracking().FirstOrDefaultAsync(filtres, token);
        }

        public Task<IQueryable<Author>> GetAllAsync(Expression<Func<Author, bool>>? filtres,
                                                CancellationToken token = default,
                                                params Expression<Func<Author, object>>[]? includesProperties)
        {
            if (filtres is null)
                return Task.FromResult(_authors.AsNoTracking());

            return Task.FromResult(_authors.AsNoTracking().Where(filtres));
        }

        public Task<Guid> UpdateAsync(Author entity, CancellationToken token = default)
        {
            if (entity.Books.Count != 0)
            {
                _context.Books.AttachRange(entity.Books);
            }

            _authors.Update(entity);

            return Task.FromResult(entity.Id);
        }
    }
}
