using FloykLibrary.Application.Shared.Abstractions;
using FloykLibrary.Application.Shared.Models;
using FloykLibrary.Application.Shared.Models.DTOs;
using FloykLibrary.Application.Users.Commands.Login;
using FloykLibrary.Application.Users.Commands.RefreshToken;
using FloykLibrary.Application.Users.Commands.Registration;
using FloykLibrary.Application.Users.Queries.GetUserById;
using FloykLibrary.Application.Users.Queries.GetUsersWithPagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FloykLibrary.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUsersAsync([FromQuery] int pageNumber = 1,
                                                                [FromQuery] int pageSize = 3,
                                                                CancellationToken token = default)
        {
            PaginatedResult<UserDTO> response = await _mediator.Send(new GetUsersWithPaginationQuery() { PageNumber = pageNumber, PageSize = pageSize }, token);

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] Guid id, CancellationToken token = default)
        {
            UserDTO response = await _mediator.Send(new GetUserByIdQuery() { Id = id }, token);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginCommand loginCommand, CancellationToken token = default)
        {
            LoginCommandResponse response = await _mediator.Send(loginCommand, token);

            if (response.IsLogedIn)
                return Ok(response);

            return Unauthorized();
        }

        [HttpPost("registration")]
        public async Task<IActionResult> RegistrationAsync([FromBody] RegistrationCommand registrationCommand, CancellationToken token = default)
        {
            RegistrationCommandResponse response =  await _mediator.Send(registrationCommand, token);

            return Ok(response);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenCommand refreshTokenCommand, CancellationToken token = default)
        {
            RefreshTokenCommandResponse response = await _mediator.Send(refreshTokenCommand, token);

            return Ok(response);
        }
    }
}
