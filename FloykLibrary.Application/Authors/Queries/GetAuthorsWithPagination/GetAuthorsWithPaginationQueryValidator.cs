using FluentValidation;

namespace FloykLibrary.Application.Authors.Queries.GetAuthorsWithPagination
{
    public class GetAuthorsWithPaginationQueryValidator : AbstractValidator<GetAuthorsWithPaginationQuery>
    {
        public GetAuthorsWithPaginationQueryValidator()
        {
            RuleFor(q => q.PageNumber).NotEmpty().GreaterThan(0);

            RuleFor(q => q.PageSize).NotEmpty().GreaterThan(0);
        }
    }
}
