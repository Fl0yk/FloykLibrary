using FloykLibrary.Application.Shared.Models;
using FloykLibrary.Application.Shared.Models.DTOs;
using MediatR;

namespace FloykLibrary.Application.Users.Queries.GetUsersWithPagination
{
    public class GetUsersWithPaginationQuery : IRequest<PaginatedResult<UserDTO>>
    {
        public required int PageSize { get; init; }

        public required int PageNumber { get; init; }
    }
}
