using MediatR;

namespace FloykLibrary.Application.Books.Commands.DeleteBook
{
    public class DeleteBookCommand : IRequest
    {
        public required Guid Id { get; init; }
    }
}
