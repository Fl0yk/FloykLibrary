using AutoMapper;
using FloykLibrary.Application.Shared.Models.DTOs;
using FloykLibrary.Domain.Entities;

namespace FloykLibrary.Application.Shared.Mapper.UserMapping
{
    internal class UserToDTO : Profile
    {
        public UserToDTO() 
        {
            CreateMap<User, UserDTO>();

            CreateMap<Role, InnerRoleDTO>();

            CreateMap<Book, BookDTO>();
        }
    }
}
