using AutoMapper;
using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;
using MediatR;

namespace FloykLibrary.Application.Books.Commands.UpdateBook
{
    internal class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Guid>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBookCommandHandler(IBookRepository bookRepository, 
                                        IAuthorRepository authorRepository, 
                                        IUnitOfWork unitOfWork, 
                                        IMapper mapper)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(UpdateBookCommand request, CancellationToken token)
        {
            Book book = _mapper.Map<Book>(request);

            Book? dbBook = await _bookRepository.FirstOrDefaultAsync(b => b.Id == book.Id, token);

            if (dbBook is null)
                throw new KeyNotFoundException($"Book with id {book.Id} not found");

            IQueryable<Author> dbAuthors = await _authorRepository
                            .GetAllAsync(author =>
                                book.Authors
                                    .Any(innerAuthor => innerAuthor.Id == author.Id),
                                token);
            
            if (dbAuthors.Count() != book.Authors.Count)
            {
                IEnumerable<Guid> invalidIds = book.Authors
                                                .Select(author => author.Id)
                                                .Except(dbAuthors.Select(a => a.Id));

                throw new KeyNotFoundException($"Authors with id {String.Join(", ", invalidIds)} not found");
            }

            Guid id = await _bookRepository.UpdateAsync(book, token);

            await _unitOfWork.SaveChangesAsync(token);

            return id;
        }
    }
}
