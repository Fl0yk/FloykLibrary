using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;
using MediatR;

namespace FloykLibrary.Application.Books.Commands.DeleteBook
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _bookRepository = unitOfWork.BookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteBookCommand request, CancellationToken token)
        {
            Book? dbBook = await _bookRepository.FirstOrDefaultAsync(b => b.Id == request.Id, token);

            if (dbBook is null)
                throw new KeyNotFoundException($"Book with id {request.Id} not found");

            await _bookRepository.DeleteAsync(dbBook, token);

            await _unitOfWork.SaveChangesAsync(token);
        }
    }
}
