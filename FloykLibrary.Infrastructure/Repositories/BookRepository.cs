using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;
using FloykLibrary.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace FloykLibrary.Infrastructure.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context) { }

        public override Task<Guid> CreateAsync(Book entity, CancellationToken token = default)
        {
            if (entity.Authors.Count > 0)
            {
                _context.Authors.AttachRange(entity.Authors);
            }

            _entities.Add(entity);

            return Task.FromResult(entity.Id);
        }

        public async Task<Book?> GetBookByIdWithAuthors(Guid bookId,
            CancellationToken token = default) => 
            await ApplySpecification(
                new GetBookByIdWithAuthorsSpecification(bookId))
            .FirstOrDefaultAsync(token);

        public Task<IQueryable<Book>> GetFiltredBooks(string? namePart,
            string? descriptionPart,
            CancellationToken token = default) => Task.FromResult(
                ApplySpecification([
                new GetBooksByNameContainsSpecification(namePart),
                new GetBooksByDescriptionContainsSpecification(descriptionPart)
            ]));
           

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

        private IQueryable<Book> ApplySpecification(Specification<Book> specification)
        {
            return SpecificationEvaluator.GetQuery(_entities, specification);
        }

        private IQueryable<Book> ApplySpecification(IEnumerable<Specification<Book>> specifications)
        {
            IQueryable<Book> query = _entities;

            foreach (var  specification in specifications)
            {
                query = SpecificationEvaluator.GetQuery(query, specification);
            }

            return query;
        }
    }
}
