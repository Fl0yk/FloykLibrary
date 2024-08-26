using FluentValidation;

namespace FloykLibrary.Application.Authors.Queries.GetAuthorById
{
    public class GetAuthorByIdQueryValidator : AbstractValidator<GetAuthorByIdQuery>
    {
        public GetAuthorByIdQueryValidator()
        {
            RuleFor(a => a.Id).NotEmpty();
        }
    }
}
