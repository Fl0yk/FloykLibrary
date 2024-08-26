using FluentValidation;

namespace FloykLibrary.Application.Books.Commands.AddImage
{
    public class AddImageCommandValidator : AbstractValidator<AddImageCommand>
    {
        public AddImageCommandValidator() 
        {
            RuleFor(b => b.Id).NotEmpty();
        }
    }
}
