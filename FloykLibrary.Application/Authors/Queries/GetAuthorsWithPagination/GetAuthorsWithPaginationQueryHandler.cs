using AutoMapper;
using FloykLibrary.Application.Shared.Models;
using FloykLibrary.Application.Shared.Models.DTOs;
using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;
using MediatR;

namespace FloykLibrary.Application.Authors.Queries.GetAuthorsWithPagination
{
    public class GetAuthorsWithPaginationQueryHandler
                : IRequestHandler<GetAuthorsWithPaginationQuery, PaginatedResult<AuthorDTO>>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public GetAuthorsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _authorRepository = unitOfWork.AuthorRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<AuthorDTO>> Handle(GetAuthorsWithPaginationQuery request,
                                                                CancellationToken token)
        {
            IQueryable<Author> query = await _authorRepository.GetAllAsync(null, token, a => a.Books);

            int count = query.Count();

            List<Author> authors = query
                .OrderBy(b => b.Id)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return new()
            {
                Items = _mapper.Map<IEnumerable<AuthorDTO>>(authors),
                CurrentPage = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling(count / (double)request.PageSize)
            };
        }
    }
}
