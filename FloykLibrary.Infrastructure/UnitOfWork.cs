using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Infrastructure.Repositories;

namespace FloykLibrary.Infrastructure
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        private IUserRepository _userRepository;
        private IBookRepository _bookRepository;
        private IAuthorRepository _authorRepository;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUserRepository UserRepository => 
            _userRepository ??= new UserRepository(_dbContext);

        public IBookRepository BookRepository => 
            _bookRepository ??= new BookRepository(_dbContext);

        public IAuthorRepository AuthorRepository => 
            _authorRepository ??= new AuthorRepository(_dbContext);

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
