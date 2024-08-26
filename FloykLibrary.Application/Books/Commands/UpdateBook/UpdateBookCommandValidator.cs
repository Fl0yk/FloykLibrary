using FluentValidation;

namespace FloykLibrary.Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(b => b.Id).NotEmpty();

            RuleFor(b => b.ISBN).NotEmpty()
                .Must(CheckLength)
                    .WithMessage("'{PropertyName}' must be equal 10 or 13");

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
    }
}
