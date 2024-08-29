using AutoMapper;
using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;
using MediatR;

namespace FloykLibrary.Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Guid>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAuthorCommandHandler(IAuthorRepository authorRepository, 
                                            IUnitOfWork unitOfWork, 
                                            IMapper mapper)
        {
            _authorRepository = authorRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateAuthorCommand request, CancellationToken token)
        {
            Author author = _mapper.Map<Author>(request);

            Guid id = await _authorRepository.CreateAsync(author, token);
            await _unitOfWork.SaveChangesAsync(token);

            return id;
        }
    }
}
