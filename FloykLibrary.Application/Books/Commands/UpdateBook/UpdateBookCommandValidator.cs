using FloykLibrary.Domain.Abstractions;
using FluentValidation;

namespace FloykLibrary.Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        private readonly IBookRepository _bookRepository;

        public UpdateBookCommandValidator(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;

            RuleFor(b => b.ISBN).NotEmpty()
                .Must(CheckLength)
                    .WithMessage("'{PropertyName}' must be equal 10 or 13")
                .MustAsync(CheckIsbnAsync)
                    .WithMessage("'{PropertyName}' must be unique");

            RuleFor(b => b.Title).NotEmpty();

            RuleFor(b => b.Description).NotEmpty();

            RuleFor(b => b.Genre).NotEmpty();

            RuleFor(b => b.Authors).NotEmpty();
        }

        private static bool CheckLength(UpdateBookCommand command, string isbn)
        {
            if (isbn.Length == 10 || isbn.Length == 13)
                return true;

            return false;
        }

        private Task<bool> CheckIsbnAsync(UpdateBookCommand command, string isbn, CancellationToken token)
        {
            return _bookRepository.IsIsbnUniqueAsync(isbn, token);
        }
    }
}
