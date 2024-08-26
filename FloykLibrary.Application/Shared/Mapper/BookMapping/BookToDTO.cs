using AutoMapper;
using FloykLibrary.Application.Shared.Models.DTOs;
using FloykLibrary.Domain.Entities;

namespace FloykLibrary.Application.Shared.Mapper.BookMapping
{
    public class BookToDTO : Profile
    {
        public BookToDTO() 
        {
            CreateMap<Book, BookDTO>();
            CreateMap<Author, InnerAuthorDTO>();
        }
    }
}
