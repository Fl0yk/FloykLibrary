using FluentValidation;

namespace FloykLibrary.Application.Users.Queries.GetUsersWithPagination
{
    public class GetUsersWithPaginationQueryValidator : AbstractValidator<GetUsersWithPaginationQuery>
    {
        public GetUsersWithPaginationQueryValidator()
        {
            RuleFor(q => q.PageNumber).NotEmpty().GreaterThan(0);

            RuleFor(q => q.PageSize).NotEmpty().GreaterThan(0);
        }
    }
}
