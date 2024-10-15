using FloykLibrary.Domain.Entities;

namespace FloykLibrary.Infrastructure.Specifications
{
    public class GetBooksByDescriptionContainsSpecification
        : Specification<Book>
    {
        public GetBooksByDescriptionContainsSpecification(string? descriptionPart)
            : base (book => 
            descriptionPart == null || 
            book.Description.Contains(descriptionPart))
        {

        }
    }
}
