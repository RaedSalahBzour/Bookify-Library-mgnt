using Application.Authorization.Commands.Auth;
using Application.Authorization.Dtos.Token;
using Application.Users.Commands;
using Application.Users.Dtos;
using Application.Users.Queries;
using Application.Users.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace Bookify_Library_mgnt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class AuthController : ControllerBase
    {
        private readonly ISender _sender;

        public AuthController(ISender sender)
        {
            _sender = sender;
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers(int pageNumber = 1, int pageSize = 10)
        {
            return Ok(await _sender.Send(new GetUsersQuery(pageNumber, pageSize)));

        }

        [HttpGet("users/{id:guid}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var result = await _sender.Send(new GetUserByIdQuery(id));
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return Ok(result.Data);
        }
        [AllowAnonymous]
        [HttpPost("users")]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
        {
            var result = await _sender.Send(command);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return CreatedAtAction(nameof(GetUserById), new { Id = result.Data.Id }, result.Data);
        }

        [HttpPut("users/{id:guid}")]
        [Authorize(Roles = "superAdmin")]
        public async Task<IActionResult> UpdateUser([FromRoute] string id, [FromBody] UpdateUserCommand command)
        {
            command.id = id;
            var result = await _sender.Send(command);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return Ok(result.Data);

        }
        [HttpDelete("users/{id:guid}")]
        [Authorize(Roles = "superAdmin")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            var result = await _sender.Send(new DeleteUserCommand(id));
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return Ok(result.Data);
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _sender.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefrshToken([FromBody] RefreshTokenCommand command)
        {
            var result = await _sender.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }


    }
}
