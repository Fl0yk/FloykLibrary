using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FloykLibrary.Infrastructure.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context) { }

        public Task AddImageAsync(Guid guidId, string image, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public override Task<Guid> CreateAsync(Book entity, CancellationToken token = default)
        {
            if (entity.Authors.Count > 0)
            {
                _context.Authors.AttachRange(entity.Authors);
            }

            _entities.Add(entity);

            return Task.FromResult(entity.Id);
        }

        public async Task<bool> IsIsbnUniqueAsync(string isbn, CancellationToken token = default)
        {
            return !await _entities.AnyAsync(b => b.ISBN  == isbn, token);
        }

        public override Task<Guid> UpdateAsync(Book entity, CancellationToken token = default)
        {
            if (entity.Authors.Count > 0)
            {
                _context.Authors.AttachRange(entity.Authors);
            }

            _entities.Update(entity);

            return Task.FromResult(entity.Id);
        }
    }
}
