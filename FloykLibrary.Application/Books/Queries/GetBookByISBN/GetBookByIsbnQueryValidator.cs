using FluentValidation;

namespace FloykLibrary.Application.Books.Queries.GetBookByISBN
{
    public class GetBookByIsbnQueryValidator : AbstractValidator<GetBookByIsbnQuery>
    {
        public GetBookByIsbnQueryValidator() 
        {
            RuleFor(b => b.ISBN).NotEmpty()
                .Must(CheckLength)
                    .WithMessage("'{PropertyName}' must be equal 10 or 13");
        }
        private static bool CheckLength(GetBookByIsbnQuery query, string isbn)
        {
            if (isbn.Length == 10 || isbn.Length == 13)
                return true;

            return false;
        }
    }
}
