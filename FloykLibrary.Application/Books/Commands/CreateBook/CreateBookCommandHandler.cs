using AutoMapper;
using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;
using MediatR;

namespace FloykLibrary.Application.Books.Commands.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Guid>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBookCommandHandler(IBookRepository bookRepository, 
                                        IAuthorRepository authorRepository, 
                                        IUnitOfWork unitOfWork,
                                        IMapper mapper)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateBookCommand request, CancellationToken token)
        {
            Book book = _mapper.Map<Book>(request);

            List<Author> authors = (await _authorRepository
                                .GetAllAsync(a => request.AuthorsId.Contains(a.Id), token))
                                .ToList();

            if (authors.Count != request.AuthorsId.Count)
            {
                IEnumerable<Guid> invalidIds = request.AuthorsId.Except(authors.Select(a => a.Id));

                throw new KeyNotFoundException($"Authors with id {String.Join(", ", invalidIds)} not found");
            }

            book.Authors.AddRange(authors);

            Guid id = await _bookRepository.CreateAsync(book, token);

            await _unitOfWork.SaveChangesAsync(token);

            return id;
        }
    }
}
