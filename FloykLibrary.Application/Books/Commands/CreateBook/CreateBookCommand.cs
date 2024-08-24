using MediatR;

namespace FloykLibrary.Application.Books.Commands.CreateBook
{
    public record class CreateBookCommand(
        string ISBN, 
        string Title,
        string Description,
        string Genre) : IRequest<Guid>;
}
