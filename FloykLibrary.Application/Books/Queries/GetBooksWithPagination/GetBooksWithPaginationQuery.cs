using FloykLibrary.Application.Shared.Models;
using FloykLibrary.Application.Shared.Models.DTOs;
using MediatR;

namespace FloykLibrary.Application.Books.Queries.GetBooksWithPagination
{
    public class GetBooksWithPaginationQuery : IRequest<PaginatedResult<BookDTO>>
    {
        public int PageSize { get; init; }

        public int PageNumber { get; init; }

        public GetBooksWithPaginationQuery(int pageSize, int pageNumber)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
}
