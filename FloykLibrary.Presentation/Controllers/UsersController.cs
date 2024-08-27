using FloykLibrary.Application.Users.Commands.Login;
using FloykLibrary.Application.Users.Commands.RefreshToken;
using FloykLibrary.Application.Users.Commands.Registration;
using MediatR;
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

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginCommand loginCommand, CancellationToken token)
        {
            LoginCommandResponse response = await _mediator.Send(loginCommand, token);

            if (response.IsLogedIn)
                return Ok(response);

            return Unauthorized();
        }

        [HttpPost("registration")]
        public async Task<IActionResult> RegistrationAsync([FromBody] RegistrationCommand registrationCommand, CancellationToken token)
        {
            RegistrationCommandResponse response =  await _mediator.Send(registrationCommand, token);

            return Ok(response);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenCommand refreshTokenCommand, CancellationToken token)
        {
            RefreshTokenCommandResponse response = await _mediator.Send(refreshTokenCommand, token);

            return Ok(response);
        }
    }
}
