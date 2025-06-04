using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Dtos.Claims;
using System.Security.Claims;

namespace Bookify_Library_mgnt.Services.Interfaces
{
    public interface IClaimService
    {
        Task<Result<IList<Claim>>> GetUserClaimsAsync(string userId);
        Task<Result<string>> AddClaimToUserAsync(AddClaimToUserDto addClaimDto);
        Task<Result<string>> RemoveClaimFromUserAsync(string userId, string claimType);
    }
}
