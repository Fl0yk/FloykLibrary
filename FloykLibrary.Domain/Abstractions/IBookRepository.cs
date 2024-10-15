using FloykLibrary.Domain.Entities;

namespace FloykLibrary.Domain.Abstractions
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        public Task<bool> IsIsbnUniqueAsync(string isbn, CancellationToken token = default);

        public Task<Book?> GetBookByIdWithAuthors(Guid bookId, CancellationToken token = default);

        public Task<IQueryable<Book>> GetFiltredBooks(string? namePart, string? descriptionPart, CancellationToken token = default);
    }
}
