using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;

namespace FloykLibrary.Infrastructure.Repositories
{
    class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }

        public override Task<Guid> CreateAsync(User entity, CancellationToken token = default)
        {
            if (entity.Roles.Any())
            {
                _context.Roles.AttachRange(entity.Roles);
            }

            return base.CreateAsync(entity, token);
        }

        public override Task<Guid> UpdateAsync(User entity, CancellationToken token = default)
        {
            _entities.Attach(entity);

            return base.UpdateAsync(entity, token);
        }
    }
}
