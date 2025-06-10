using Application.Authorization.Dtos.Claims;
using System.Security.Claims;

namespace Application.Authorization.Services;

public interface IClaimService
{
    Task<IList<Claim>> GetUserClaimsAsync(string userId);
    Task<string> AddClaimToUserAsync(AddClaimToUserDto addClaimDto);
    Task<string> RemoveClaimFromUserAsync(string userId, string claimType);
}
