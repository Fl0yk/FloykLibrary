using MediatR;

namespace FloykLibrary.Application.Books.Commands.TakeBook
{
    public class TakeBookCommand : IRequest
    {
        public required Guid Id { get; init; }

        public required DateTime ReturningBook { get; init; }
    }
}
