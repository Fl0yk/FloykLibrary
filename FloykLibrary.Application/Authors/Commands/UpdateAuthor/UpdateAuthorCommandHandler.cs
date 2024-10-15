using AutoMapper;
using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;
using MediatR;

namespace FloykLibrary.Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Guid>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAuthorCommandHandler(IUnitOfWork unitOfWork, 
                                            IMapper mapper)
        {
            _authorRepository = unitOfWork.AuthorRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(UpdateAuthorCommand request, CancellationToken token)
        {
            Author author = _mapper.Map<Author>(request);

            Author? dbAuthor = await _authorRepository.FirstOrDefaultAsync(a => a.Id == author.Id, token);

            if (dbAuthor is null)
                throw new KeyNotFoundException($"Author with id {author.Id} not found");

            await _authorRepository.UpdateAsync(author, token);

            await _unitOfWork.SaveChangesAsync(token);

            return author.Id;
        }
    }
}
