using MediatR;

namespace FloykLibrary.Application.Books.Commands.TakeBook
{
    public class TakeBookCommand : IRequest
    {
        public required Guid BookId { get; init; }

        public required Guid UserId { get; init; }

        public required DateTime ReturningBook { get; init; }
    }
}
