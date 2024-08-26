using FloykLibrary.Application.Authors.Commands.CreateAuthor;
using FluentValidation;

namespace FloykLibrary.Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(a => a.Name).NotEmpty();

            RuleFor(a => a.Surname).NotEmpty();

            RuleFor(a => a.Country).NotEmpty();

            RuleFor(a => a.DateOfBirth)
                .NotEmpty()
                .LessThan(DateTime.Now);
        }
    }
}
