using AutoMapper;
using FloykLibrary.Application.Books.Commands.UpdateBook;
using FloykLibrary.Domain.Entities;

namespace FloykLibrary.Application.Shared.Mapper.BookMapping
{
    public class UpdateBookCommandToBook : Profile
    {
        public UpdateBookCommandToBook()
        {
            CreateMap<UpdateBookCommand, Book>();
            CreateMap<UpdateBookInnerAuthor, Author>();
        }
    }
}
