using FloykLibrary.Domain.Entities;

namespace FloykLibrary.Domain.Abstractions
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        public Task<bool> IsIsbnUniqueAsync(string isbn, CancellationToken token = default);
    }
}
