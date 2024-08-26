using FloykLibrary.Application.Shared.Abstractions;
using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;
using MediatR;

namespace FloykLibrary.Application.Books.Commands.AddImage
{
    public class AddImageCommandHandler : IRequestHandler<AddImageCommand>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;

        public AddImageCommandHandler(IBookRepository bookRepository, 
                                        IUnitOfWork unitOfWork, 
                                        IImageService imageService)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
            _imageService = imageService;
        }

        public async Task Handle(AddImageCommand request, CancellationToken token)
        {
            Book? dbBook = await _bookRepository.FirstOrDefaultAsync(b => b.Id == request.Id);

            if (dbBook is null)
                throw new KeyNotFoundException($"Book with id {request.Id} not found");

            string imagePath = await _imageService.SaveImageAsync(request.ImageStream, request.FileName, dbBook.Image, token);

            dbBook.Image = imagePath;

            await _bookRepository.UpdateAsync(dbBook, token);

            await _unitOfWork.SaveChangesAsync(token);
        }
    }
}
