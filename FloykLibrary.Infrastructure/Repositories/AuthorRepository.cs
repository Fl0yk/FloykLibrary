using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;

namespace FloykLibrary.Infrastructure.Repositories
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationDbContext context) : base(context) { }

        public override Task<Guid> CreateAsync(Author entity, CancellationToken token = default)
        {
            if (entity.Books.Count != 0)
            {
                _context.Books.AttachRange(entity.Books);
            }

            _entities.Add(entity);

            return Task.FromResult(entity.Id);
        }

        public override Task<Guid> UpdateAsync(Author entity, CancellationToken token = default)
        {
            if (entity.Books.Count != 0)
            {
                _context.Books.AttachRange(entity.Books);
            }

            _entities.Update(entity);

            return Task.FromResult(entity.Id);
        }
    }
}
