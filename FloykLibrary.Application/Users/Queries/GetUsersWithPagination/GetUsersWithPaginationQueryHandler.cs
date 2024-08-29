using AutoMapper;
using FloykLibrary.Application.Shared.Models;
using FloykLibrary.Application.Shared.Models.DTOs;
using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Domain.Entities;
using MediatR;

namespace FloykLibrary.Application.Users.Queries.GetUsersWithPagination
{
    public class GetUsersWithPaginationQueryHandler : IRequestHandler<GetUsersWithPaginationQuery, PaginatedResult<UserDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUsersWithPaginationQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<UserDTO>> Handle(GetUsersWithPaginationQuery request, CancellationToken cancellationToken)
        {
            IQueryable<User> query = await _userRepository.GetAllAsync(null, cancellationToken, u => u.TakenBooks, u => u.Roles);

            int count = query.Count();

            List<User> users = query
                .OrderBy(u => u.Id)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return new()
            {
                Items = _mapper.Map<IEnumerable<UserDTO>>(users),
                CurrentPage = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling(count / (double)request.PageSize)
            };
        }
    }
}
