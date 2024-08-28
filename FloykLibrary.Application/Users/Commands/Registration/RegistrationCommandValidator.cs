using FloykLibrary.Domain.Abstractions;
using FluentValidation;

namespace FloykLibrary.Application.Users.Commands.Registration
{
    public class RegistrationCommandValidator : AbstractValidator<RegistrationCommand>
    {
        private readonly IUserRepository _userRepository;

        public RegistrationCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(u => u.Email)
                .NotEmpty()
                .MaximumLength(30)
                .EmailAddress()
                .MustAsync(CheckEmailAsync)
                .WithMessage("Email should be unique");

            RuleFor(u => u.Name)
                .NotEmpty()
                .MaximumLength(30);
        }

        private async Task<bool> CheckEmailAsync(string email, CancellationToken token)
        {
            return await _userRepository.FirstOrDefaultAsync(x => x.Email == email) is null;
        }
    }
}
