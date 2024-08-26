using AutoMapper;
using FloykLibrary.Application.Authors.Commands.CreateAuthor;
using FloykLibrary.Domain.Entities;

namespace FloykLibrary.Application.Shared.Mapper.AuthorMapping
{
    public class CreateAuthorCommandToAuthor : Profile
    {
        public CreateAuthorCommandToAuthor()
        {
            CreateMap<CreateAuthorCommand, Author>();
        }
    }
}
