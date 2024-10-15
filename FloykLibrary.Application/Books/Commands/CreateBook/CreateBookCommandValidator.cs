using FluentValidation;

namespace FloykLibrary.Application.Books.Commands.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(b => b.ISBN).NotEmpty()
                .Must(CheckLength)
                    .WithMessage("'{PropertyName}' must be equal 10 or 13");

            RuleFor(b => b.Title).NotEmpty();

            RuleFor(b => b.Description).NotEmpty();

            RuleFor(b => b.Genre).NotEmpty();

            RuleFor(b => b.AuthorsId).NotEmpty();
        }

        private static bool CheckLength(CreateBookCommand command, string isbn)
        {
            if (isbn.Length == 10 || isbn.Length == 13)
                return true;

            return false;
        }
    }
}
