using FloykLibrary.Application.Shared.Models.DTOs;
using MediatR;

namespace FloykLibrary.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserDTO>
    {
        public required Guid Id { get; init; }
    }
}
