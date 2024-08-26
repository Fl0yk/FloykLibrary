using FloykLibrary.Domain.Entities;
using System.Linq.Expressions;

namespace FloykLibrary.Domain.Abstractions
{
    public interface IBaseRepository<T> where T : Entity
    {
        public Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>>? filtres, 
                                                CancellationToken token = default, 
                                                params Expression<Func<T, object>>[]? includesProperties);

        public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filtres, 
                                                CancellationToken token = default, 
                                                params Expression<Func<T, object>>[]? includesProperties);

        public Task<Guid> CreateAsync(T entity, CancellationToken token = default);

        public Task DeleteAsync(T entity, CancellationToken token = default);

        public Task<Guid> UpdateAsync(T entity, CancellationToken token = default);
    }
}
