using MediatR;

namespace FloykLibrary.Application.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand : IRequest
    {
        public required Guid Id { get; init; }
    }
}
