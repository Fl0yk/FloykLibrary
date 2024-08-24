using AutoMapper;
using FloykLibrary.Application.Shared.Models;
using FloykLibrary.Application.Shared.Models.DTOs;
using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;
using MediatR;

namespace FloykLibrary.Application.Books.Queries.GetBooksWithPagination
{
    public class GetBooksWithPaginationQueryHandler : IRequestHandler<GetBooksWithPaginationQuery, PaginatedResult<BookDTO>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetBooksWithPaginationQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<BookDTO>> Handle(GetBooksWithPaginationQuery request, CancellationToken token)
        {
            IQueryable<Book> query = await _bookRepository.GetAllAsync(null, token, b => b.Authors);
            
            int count = query.Count();
            List<Book> books = query
                .OrderBy(b => b.Id)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return new()
            {
                Items = _mapper.Map<IEnumerable<BookDTO>>(books),
                CurrentPage = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling(count / (double)request.PageSize)
            };
        }
    }
}
