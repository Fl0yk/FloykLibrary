using FloykLibrary.Domain.Entities;

namespace FloykLibrary.Infrastructure.Specifications
{
    public class GetBookByIdWithAuthorsSpecification
        : Specification<Book>
    {
        public GetBookByIdWithAuthorsSpecification(Guid bookId)
            : base(book => book.Id == bookId)
        {
            AddInclude(book => book.Authors);
        }
    }
}
