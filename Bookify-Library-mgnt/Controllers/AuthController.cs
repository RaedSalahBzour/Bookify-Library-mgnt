using Bookify_Library_mgnt.Dtos.Users;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Bookify_Library_mgnt.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Bookify_Library_mgnt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpGet()]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetUsers(int pageNumber = 1, int pageSize = 10)
        {
            return Ok(await _authService.GetUsersAsync(pageNumber, pageSize));
            var userClaims = User.Claims.Select(c => $"{c.Type}: {c.Value}");

        }

        [HttpGet("getUser/{id:guid}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var result = await _authService.GetUserByIdAsync(id);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return Ok(result.Data);
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto userDto)
        {
            var result = await _authService.CreateAsync(userDto);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return CreatedAtAction(nameof(GetUserById), new { Id = result.Data.Id }, userDto);
        }

        [HttpPut("update/{id:guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] string id, [FromBody] UpdateUserDto userDto)
        {
            var result = await _authService.UpdateUserAsync(id, userDto);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return Ok(result.Data);

        }
        [HttpDelete("delete/{id:guid}")]
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
        //[HttpPost("logout")]
        //public async Task<IActionResult> Logout()
        //{

        //}

    }
}
