namespace FloykLibrary.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public IBookRepository BookRepository { get; }
        public IAuthorRepository AuthorRepository { get; }

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
