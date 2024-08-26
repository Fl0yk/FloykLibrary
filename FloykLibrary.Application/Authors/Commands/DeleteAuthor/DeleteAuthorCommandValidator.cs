using FluentValidation;

namespace FloykLibrary.Application.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator() 
        {
            RuleFor(a => a.Id).NotEmpty();
        }
    }
}
