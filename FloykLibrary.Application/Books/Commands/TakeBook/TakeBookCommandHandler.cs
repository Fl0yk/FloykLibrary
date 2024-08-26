using FloykLibrary.Application.Shared.Exceptions;
using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;
using MediatR;

namespace FloykLibrary.Application.Books.Commands.TakeBook
{
    public class TakeBookCommandHandler : IRequestHandler<TakeBookCommand>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TakeBookCommandHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(TakeBookCommand request, CancellationToken token)
        {
            Book? dbBook = await _bookRepository.FirstOrDefaultAsync(b => b.Id == request.Id);

            if (dbBook is null)
                throw new KeyNotFoundException($"Book with id {request.Id} not found");

            if (dbBook.IsTaken)
                throw new BookIsTakenException();

            dbBook.TakingBook = DateTime.UtcNow;
            dbBook.ReturningBook = request.ReturningBook;

            await _bookRepository.UpdateAsync(dbBook, token);

            await _unitOfWork.SaveChangesAsync(token);
        }
    }
}
