using FloykLibrary.Domain.Entities;

namespace FloykLibrary.Infrastructure.Specifications
{
    public class GetBooksByNameContainsSpecification : Specification<Book>
    {
        public GetBooksByNameContainsSpecification(string? namePart)
            : base(book => namePart == null || book.Title.Contains(namePart))
        {

        }
    }
}
