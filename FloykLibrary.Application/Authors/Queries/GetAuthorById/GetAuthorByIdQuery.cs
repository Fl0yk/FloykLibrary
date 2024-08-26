using FloykLibrary.Application.Shared.Models.DTOs;
using MediatR;

namespace FloykLibrary.Application.Authors.Queries.GetAuthorById
{
    public class GetAuthorByIdQuery : IRequest<AuthorDTO>
    {
        public required Guid Id { get; init; }
    }
}
