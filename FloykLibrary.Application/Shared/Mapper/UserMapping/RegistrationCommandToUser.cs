using AutoMapper;
using FloykLibrary.Application.Users.Commands.Registration;
using FloykLibrary.Domain.Entities;

namespace FloykLibrary.Application.Shared.Mapper.UserMapping
{
    public class RegistrationCommandToUser : Profile
    {
        public RegistrationCommandToUser() 
        {
            CreateMap<RegistrationCommand, User>();
        }
    }
}
