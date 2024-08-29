using FluentValidation;

namespace FloykLibrary.Application.Users.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator() 
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(u => u.Email)
                .NotEmpty()
                .MaximumLength(30);
        }
    }
}
