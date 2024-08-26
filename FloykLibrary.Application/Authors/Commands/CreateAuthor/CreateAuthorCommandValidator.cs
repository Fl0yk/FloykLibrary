using FluentValidation;

namespace FloykLibrary.Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator() 
        {
            RuleFor(a => a.Name).NotEmpty();

            RuleFor(a => a.Surname).NotEmpty();

            RuleFor(a => a.Country).NotEmpty();

            RuleFor(a => a.DateOfBirth.Date)
                .NotEmpty()
                .LessThan(DateTime.UtcNow.Date);
        }
    }
}
