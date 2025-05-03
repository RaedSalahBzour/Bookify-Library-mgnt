using Bookify_Library_mgnt.Dtos.Users;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookify_Library_mgnt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _authRepository.GetUsersAsync());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var user = await _authRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CreateUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _authRepository.CreateAsync(userDto);
            if (user == null)
            {
                return BadRequest("User could not be created. Email or username may already be in use.");
            }
            return CreatedAtAction(nameof(GetUserById), new { Id = user.Id }, user);

        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] string id, [FromBody] UpdateUserDto userDto)
        {
            var user = await _authRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            await _authRepository.UpdateUserAsync(id, userDto);
            return Ok(userDto);

        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            var user = await _authRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            await _authRepository.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
