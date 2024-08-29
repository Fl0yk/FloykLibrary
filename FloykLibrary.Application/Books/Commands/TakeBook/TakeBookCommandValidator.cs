using FluentValidation;

namespace FloykLibrary.Application.Books.Commands.TakeBook
{
    public class TakeBookCommandValidator : AbstractValidator<TakeBookCommand>
    {
        public TakeBookCommandValidator() 
        {
            RuleFor(b => b.BookId).NotEmpty();

            RuleFor(b => b.UserId).NotEmpty();

            RuleFor(b => b.ReturningBook.Date)
                .NotEmpty()
                .GreaterThan(DateTime.UtcNow.Date);
        }
    }
}
