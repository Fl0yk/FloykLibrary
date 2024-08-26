using FloykLibrary.Application.Shared.Models;
using FloykLibrary.Application.Shared.Models.DTOs;
using MediatR;

namespace FloykLibrary.Application.Authors.Queries.GetAuthorsWithPagination
{
    public class GetAuthorsWithPaginationQuery : IRequest<PaginatedResult<AuthorDTO>>
    {
        public int PageSize { get; init; }

        public int PageNumber { get; init; }

        public GetAuthorsWithPaginationQuery(int pageSize, int pageNumber)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
}
