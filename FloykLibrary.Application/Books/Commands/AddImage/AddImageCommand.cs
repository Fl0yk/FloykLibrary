using MediatR;

namespace FloykLibrary.Application.Books.Commands.AddImage
{
    public class AddImageCommand : IRequest
    {
        public required Guid Id { get; init; }

        public required string FileName { get; init; }

        public required Stream ImageStream { get; init; }
    }
}
