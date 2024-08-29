using FluentValidation;

namespace FloykLibrary.Application.Users.Commands.RefreshToken
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator() 
        {
            RuleFor(t => t.Jwt).NotEmpty();
        }
    }
}
