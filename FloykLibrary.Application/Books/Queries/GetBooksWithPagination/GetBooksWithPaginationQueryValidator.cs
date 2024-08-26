using FluentValidation;

namespace FloykLibrary.Application.Books.Queries.GetBooksWithPagination
{
    public class GetBooksWithPaginationQueryValidator : AbstractValidator<GetBooksWithPaginationQuery>
    {
        public GetBooksWithPaginationQueryValidator()
        {
            RuleFor(q => q.PageNumber).NotEmpty().GreaterThan(0);

            RuleFor(q => q.PageSize).NotEmpty().GreaterThan(0);
        }
    }
}
