using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Dtos.Roles;
using Bookify_Library_mgnt.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Bookify_Library_mgnt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles(int pageNumber = 1, int pageSize = 10)
        {
            return Ok(await _roleService.GetRolesAsync(pageNumber, pageSize));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var result = await _roleService.GetRoleByIdAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto roleDto)
        {
            var result = await _roleService.CreateRoleAsync(roleDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateRole([FromRoute] string id, [FromBody] UpdateRoleDto roleDto)
        {
            var result = await _roleService.UpdateRoleAsync(id, roleDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "superAdmin")]
        public async Task<IActionResult> DeleteRole([FromRoute] string id)
        {
            var result = await _roleService.DeleteRoleAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }
        [HttpPost("{userId:guid}/roles")]
        [Authorize(Roles = "superAdmin")]
        public async Task<IActionResult> AddToRole([FromRoute] string userId, [FromBody] string roleName)
        {
            var result = await _roleService.AddUserToRoleAsync(userId, roleName);
            if (!result.IsSuccess)
            { return BadRequest(result.Errors); }
            return Ok(result.Data);
        }
        [HttpDelete("{userId:guid}/roles")]
        [Authorize(Roles = "superAdmin")]
        public async Task<IActionResult> RemoveFromRole([FromRoute] string userId, [FromBody] string roleName)
        {
            var result = await _roleService.RemoveUserFromRoleAsync(userId, roleName);
            if (!result.IsSuccess)
            { return BadRequest(result.Errors); }
            return Ok(result.Data);
        }
        [HttpGet]
        [Route("UserRoles")]
        [Authorize(Roles = "superAdmin")]
        public async Task<IActionResult> GetUserRoles(string email)
        {
            var result = await _roleService.GetUserRoles(email);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }
    }
}
