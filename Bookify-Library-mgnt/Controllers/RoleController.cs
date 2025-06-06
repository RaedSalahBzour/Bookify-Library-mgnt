using Application.Authorization.Commands.Roles;
using Application.Authorization.Dtos.Roles;
using Application.Authorization.Queries.Roles;
using Application.Authorization.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Bookify_Library_mgnt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class RoleController : ControllerBase
    {
        private readonly ISender _sender;

        public RoleController(IRoleService roleService, ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles(int pageNumber = 1, int pageSize = 10)
        {
            return Ok(await _sender.Send(new GetRolesQuery(pageNumber, pageSize)));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var result = await _sender.Send(new GetRoleByIdQuery(id));
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand command)
        {
            var result = await _sender.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateRole([FromRoute] string id, [FromBody] UpdateRoleCommand command)
        {
            command.id = id;
            var result = await _sender.Send(command);
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
            var result = await _sender.Send(new DeleteRoleCommand(id));
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
            var result = await _sender.Send(new AddToRoleCommand(userId, roleName));
            if (!result.IsSuccess)
            { return BadRequest(result.Errors); }
            return Ok(result.Data);
        }
        [HttpDelete("{userId:guid}/roles")]
        [Authorize(Roles = "superAdmin")]
        public async Task<IActionResult> RemoveFromRole([FromRoute] string userId, [FromBody] string roleName)
        {
            var result = await _sender.Send(new RemoveFromRoleCommand(userId, roleName));
            if (!result.IsSuccess)
            { return BadRequest(result.Errors); }
            return Ok(result.Data);
        }
        [HttpGet]
        [Route("UserRoles")]
        [Authorize(Roles = "superAdmin")]
        public async Task<IActionResult> GetUserRoles(string email)
        {
            var result = await _sender.Send(new GetUserRolesQuery(email));
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }
    }
}
