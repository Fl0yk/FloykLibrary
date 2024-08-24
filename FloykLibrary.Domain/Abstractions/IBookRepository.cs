using FloykLibrary.Domain.Entities;

namespace FloykLibrary.Domain.Abstractions
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        public Task AddImageAsync(Guid guidId, string image, CancellationToken token = default);

        public Task<bool> IsIsbnUniqueAsync(string isbn, CancellationToken token = default);
    }
}
