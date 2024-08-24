using FloykLibrary.Domain.Entities;
using System.Linq.Expressions;

namespace FloykLibrary.Domain.Abstractions
{
    public interface IBaseRepository<T> where T : Entity
    {
        public Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>>? filtres, CancellationToken token = default, params Expression<Func<T, object>>[]? includesProperties);

        public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>>? filtres, CancellationToken token = default, params Expression<Func<T, object>>[]? includesProperties);

        public Task DeleteAsync(Guid guidId);

        public Task<Guid> UpdateAsync(T obj);
    }
}
