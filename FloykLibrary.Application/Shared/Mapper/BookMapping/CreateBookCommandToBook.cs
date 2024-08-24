using AutoMapper;
using FloykLibrary.Application.Books.Commands.CreateBook;
using FloykLibrary.Domain.Entities;

namespace FloykLibrary.Application.Shared.Mapper.BookMapping
{
    public class CreateBookCommandToBook : Profile
    {
        public CreateBookCommandToBook() 
        {
            CreateMap<CreateBookCommand, Book>();
        }
    }
}
