using Application.Authorization.Dtos.Claims;
using Bookify_Library_mgnt.Common;
using System.Security.Claims;

namespace Application.Authorization.Services
{
    public interface IClaimService
    {
        Task<Result<IList<Claim>>> GetUserClaimsAsync(string userId);
        Task<Result<string>> AddClaimToUserAsync(AddClaimToUserDto addClaimDto);
        Task<Result<string>> RemoveClaimFromUserAsync(string userId, string claimType);
    }
}
