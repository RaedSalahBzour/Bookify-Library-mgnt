using Bookify_Library_mgnt.Dtos.Users;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Bookify_Library_mgnt.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookify_Library_mgnt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(int pageNumber = 1, int pageSize = 10)
        {
            return Ok(await _authService.GetUsersAsync(pageNumber, pageSize));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var result = await _authService.GetUserByIdAsync(id);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return Ok(result.Data);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
        {
            var result = await _authService.CreateAsync(userDto);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return CreatedAtAction(nameof(GetUserById), new { Id = result.Data.Id }, userDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] string id, [FromBody] UpdateUserDto userDto)
        {
            var result = await _authService.UpdateUserAsync(id, userDto);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return Ok(result.Data);

        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            var result = await _authService.DeleteUserAsync(id);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return Ok(result.Data);
        }
    }
}
