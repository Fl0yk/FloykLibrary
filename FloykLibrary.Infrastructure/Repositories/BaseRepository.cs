using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace FloykLibrary.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        protected readonly DbSet<T> _entities;
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _entities = context.Set<T>();
            _context = context;
        }

        public virtual Task<Guid> CreateAsync(T entity, CancellationToken token = default)
        {
            _entities.Add(entity);

            return Task.FromResult(entity.Id);
        }

        public virtual Task DeleteAsync(T entity, CancellationToken token = default)
        {
            _entities.Remove(entity);

            return Task.CompletedTask;
        }

        public virtual Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filtres, 
                                            CancellationToken token = default, 
                                            params Expression<Func<T, object>>[]? includesProperties)
        {
            IQueryable<T> query = _entities.AsNoTracking().AsQueryable();

            if (includesProperties is not null
                    && includesProperties.Length != 0)
            {
                foreach (var include in includesProperties)
                {
                    query = query.Include(include);
                }
            }

            return query.FirstOrDefaultAsync(filtres, token);
        }

        public virtual Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>>? filtres, 
                                                CancellationToken token = default, 
                                                params Expression<Func<T, object>>[]? includesProperties)
        {
            IQueryable<T> query = _entities.AsNoTracking().AsQueryable();

            if (includesProperties is not null
                    && includesProperties.Length != 0)
            {
                foreach (var include in includesProperties)
                {
                    query = query.Include(include);
                }
            }

            if (filtres is null)
            {
                return Task.FromResult(query);
            }

            return Task.FromResult(query.Where(filtres));
        }

        public virtual Task<Guid> UpdateAsync(T entity, CancellationToken token = default)
        {
            _entities.Update(entity);

            return Task.FromResult(entity.Id);
        }
    }
}
