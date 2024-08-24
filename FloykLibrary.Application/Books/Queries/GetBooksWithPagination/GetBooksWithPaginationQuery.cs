using FloykLibrary.Application.Shared.Models;
using FloykLibrary.Application.Shared.Models.DTOs;
using MediatR;

namespace FloykLibrary.Application.Books.Queries.GetBooksWithPagination
{
    public record class GetBooksWithPaginationQuery(
        int PageSize,
        int PageNumber) : IRequest<PaginatedResult<BookDTO>>;

}
