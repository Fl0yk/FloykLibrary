using FloykLibrary.Application.Shared.Exceptions;
using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;
using MediatR;

namespace FloykLibrary.Application.Books.Commands.TakeBook
{
    public class TakeBookCommandHandler : IRequestHandler<TakeBookCommand>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TakeBookCommandHandler(IBookRepository bookRepository, 
                                        IUserRepository userRepository, 
                                        IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task Handle(TakeBookCommand request, CancellationToken token)
        {
            Book? dbBook = await _bookRepository.FirstOrDefaultAsync(b => b.Id == request.BookId, token, b => b.User!);

            if (dbBook is null)
                throw new KeyNotFoundException($"Book with id {request.BookId} not found");

            if (dbBook.IsTaken)
                throw new BookIsTakenException();

            User? dbUser = await _userRepository.FirstOrDefaultAsync(u => u.Id == request.UserId, token, u => u.TakenBooks);

            if (dbUser is null)
                throw new KeyNotFoundException($"User with not found (take book)");

            dbBook.TakingBook = DateTime.UtcNow;
            dbBook.ReturningBook = request.ReturningBook;
            dbBook.UserId = dbUser.Id;

            await _bookRepository.UpdateAsync(dbBook, token);

            await _unitOfWork.SaveChangesAsync(token);
        }
    }
}
