using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;
using MediatR;

namespace FloykLibrary.Application.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAuthorCommandHandler(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
        {
            _authorRepository = authorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteAuthorCommand request, CancellationToken token)
        {
            Author? dbAuthor = await _authorRepository.FirstOrDefaultAsync(a => a.Id == request.Id);

            if (dbAuthor is null)
                throw new KeyNotFoundException($"Author with id {request.Id} not found");

            await _authorRepository.DeleteAsync(dbAuthor, token);
            
            await _unitOfWork.SaveChangesAsync(token);
        }
    }
}
