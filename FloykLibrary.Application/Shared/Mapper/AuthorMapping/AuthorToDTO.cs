using AutoMapper;
using FloykLibrary.Application.Shared.Models.DTOs;
using FloykLibrary.Domain.Entities;

namespace FloykLibrary.Application.Shared.Mapper.AuthorMapping
{
    public class AuthorToDTO : Profile
    {
        public AuthorToDTO() 
        {
            CreateMap<Author, AuthorDTO>();

            CreateMap<Book, InnerBookDTO>();
        }
    }
}
