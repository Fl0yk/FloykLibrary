using FloykLibrary.Application.Shared.Models.DTOs;
using MediatR;

namespace FloykLibrary.Application.Books.Queries.GetBookByISBN
{
    public class GetBookByIsbnQuery : IRequest<BookDTO>
    {
        public required string ISBN { get; init; }
    }
}
