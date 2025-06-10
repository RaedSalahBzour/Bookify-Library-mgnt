using Application.Authorization.Commands.Claims;
using Application.Authorization.Dtos.Claims;
using Application.Authorization.Queries.Claims;
using Application.Authorization.Services;
using Application.Categories.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Bookify_Library_mgnt.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(policy: "CanManageUserClaimsPolicy")]
public class ClaimController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet("claims")]
    public async Task<IActionResult> GetClaims(string userId)
    {
        IList<Claim> result = await _sender.Send(new GetClaimsQuery(userId));
        return Ok(result);
    }

    [HttpPost("claims")]
    public async Task<IActionResult> AddClaimToUser([FromBody] AddClaimToUserCommand command)
    {
        string result = await _sender.Send(command);
        return Ok(result);
    }
    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> DeleteClaimFromUser([FromRoute] string userId,
        [FromBody] string claimType)
    {
        string result = await _sender.Send(new RemoveClaimFromUserCommand { UserId = userId, ClaimType = claimType });

        return Ok(result);
    }
}
