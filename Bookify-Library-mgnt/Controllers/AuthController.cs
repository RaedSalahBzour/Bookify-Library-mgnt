using Application.Authorization.Dtos.Token;
using Application.Users.Dtos;
using Application.Users.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Bookify_Library_mgnt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
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
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var result = await _authService.LoginAsync(login);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefrshToken([FromBody] RefreshTokenRequestDto requestDto)
        {
            var result = await _authService.RefreshTokenAsync(requestDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }


    }
}
