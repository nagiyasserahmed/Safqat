using MediatR;
using Microsoft.AspNetCore.Mvc;
using Safqat.Application.Auth.Commands.Login;
using Safqat.Application.Auth.Commands.Logout;
using Safqat.Application.Auth.Commands.RefreshToken;
using Safqat.Application.Auth.Commands.Register;
using Safqat.Application.Auth.DTOs;

namespace Safqat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(ISender mediator) : ControllerBase
    {
        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register(
            [FromBody] RegisterCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(
            [FromBody] LoginCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken(
            [FromBody] RefreshTokenCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Logout(
            [FromBody] LogoutCommand command,
            CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);
            return NoContent();
        }
    }
}