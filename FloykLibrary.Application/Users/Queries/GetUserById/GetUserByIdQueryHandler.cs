using AutoMapper;
using FloykLibrary.Application.Shared.Models.DTOs;
using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;
using MediatR;

namespace FloykLibrary.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> Handle(GetUserByIdQuery request, CancellationToken token)
        {
            User? dbUser = await _userRepository.FirstOrDefaultAsync(u => u.Id == request.Id, token, u => u.Roles, u => u.TakenBooks);

            if (dbUser is null)
                throw new KeyNotFoundException($"User with id {request.Id} not found");

            return _mapper.Map<UserDTO>(dbUser);
        }
    }
}
