using Application.Authorization.Commands.Auth;
using Application.Authorization.Dtos.Token;
using Application.Users.Dtos;
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
        private readonly IAuthService _authService;
        private readonly ISender _sender;

        public AuthController(IAuthService authService, ISender sender)
        {
            _authService = authService;
            _sender = sender;
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers(int pageNumber = 1, int pageSize = 10)
        {
            return Ok(await _authService.GetUsersAsync(pageNumber, pageSize));

        }

        [HttpGet("users/{id:guid}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var result = await _authService.GetUserByIdAsync(id);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return Ok(result.Data);
        }
        [AllowAnonymous]
        [HttpPost("users")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto userDto)
        {
            var result = await _authService.CreateAsync(userDto);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return CreatedAtAction(nameof(GetUserById), new { Id = result.Data.Id }, userDto);
        }

        [HttpPut("users/{id:guid}")]
        [Authorize(Roles = "superAdmin")]
        public async Task<IActionResult> UpdateUser([FromRoute] string id, [FromBody] UpdateUserDto userDto)
        {
            var result = await _authService.UpdateUserAsync(id, userDto);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return Ok(result.Data);

        }
        [HttpDelete("users/{id:guid}")]
        [Authorize(Roles = "superAdmin")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            var result = await _authService.DeleteUserAsync(id);
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
