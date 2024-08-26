using AutoMapper;
using FloykLibrary.Application.Shared.Models.DTOs;
using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;
using MediatR;

namespace FloykLibrary.Application.Authors.Queries.GetAuthorById
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, AuthorDTO>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public GetAuthorByIdQueryHandler(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<AuthorDTO> Handle(GetAuthorByIdQuery request, CancellationToken token)
        {
            Author? dbAuthor = await _authorRepository.FirstOrDefaultAsync(a => a.Id == request.Id, token, a => a.Books);

            if (dbAuthor is null)
                throw new KeyNotFoundException($"Author with id {request.Id} not found");

            return _mapper.Map<AuthorDTO>(dbAuthor);
        }
    }
}
