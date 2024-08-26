using FluentValidation;

namespace FloykLibrary.Application.Books.Queries.GetBookById
{
    public class GetBookByIdQueryValidator : AbstractValidator<GetBookByIdQuery>
    {
        public GetBookByIdQueryValidator() 
        {
            RuleFor(b => b.Id).NotEmpty();
        }
    }
}
