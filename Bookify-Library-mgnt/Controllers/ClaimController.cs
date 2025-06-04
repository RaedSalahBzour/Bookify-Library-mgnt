using Application.Authorization.Dtos.Claims;
using Application.Authorization.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Bookify_Library_mgnt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(policy: "CanManageUserClaimsPolicy")]
    public class ClaimController : ControllerBase
    {
        private readonly IClaimService _claimService;

        public ClaimController(IClaimService claimService)
        {
            _claimService = claimService;
        }

        [HttpGet("claims")]
        public async Task<IActionResult> GetClaims(string userId)
        {
            var result = await _claimService.GetUserClaimsAsync(userId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }

        [HttpPost("claims")]
        public async Task<IActionResult> AddClaimToUser([FromBody] AddClaimToUserDto addClaimDto)
        {
            var result = await _claimService.AddClaimToUserAsync(addClaimDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }
        [HttpDelete("{userId:guid}")]
        public async Task<IActionResult> DeleteClaimFromUser([FromRoute] string userId,
            [FromBody] string claimType)
        {
            var result = await _claimService.RemoveClaimFromUserAsync(userId, claimType);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }
    }
}
