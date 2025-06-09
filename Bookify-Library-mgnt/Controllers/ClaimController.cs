using Application.Authorization.Commands.Claims;
using Application.Authorization.Dtos.Claims;
using Application.Authorization.Queries.Claims;
using Application.Authorization.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Bookify_Library_mgnt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(policy: "CanManageUserClaimsPolicy")]
    public class ClaimController : ControllerBase
    {
        private readonly ISender _sender;

        public ClaimController(IClaimService claimService, ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("claims")]
        public async Task<IActionResult> GetClaims(string userId)
        {
            var result = await _sender.Send(new GetClaimsQuery(userId));
            return Ok(result);
        }

        [HttpPost("claims")]
        public async Task<IActionResult> AddClaimToUser([FromBody] AddClaimToUserCommand command)
        {
            var result = await _sender.Send(command);
            return Ok(result);
        }
        [HttpDelete("{userId:guid}")]
        public async Task<IActionResult> DeleteClaimFromUser([FromRoute] string userId,
            [FromBody] string claimType)
        {
            var result = await _sender.Send(new RemoveClaimFromUserCommand(userId, claimType));

            return Ok(result);
        }
    }
}
