using AutoMapper;
using FloykLibrary.Application.Authors.Commands.UpdateAuthor;
using FloykLibrary.Domain.Entities;

namespace FloykLibrary.Application.Shared.Mapper.AuthorMapping
{
    public class UpdateAuthorCommandToAuthor : Profile
    {
        public UpdateAuthorCommandToAuthor() 
        {
            CreateMap<UpdateAuthorCommand, Author>();
        }
    }
}
