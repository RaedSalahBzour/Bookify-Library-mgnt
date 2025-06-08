using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Domain.Interfaces
{
    public interface IClaimRepository
    {
        Task<IdentityResult> AddClaimToUser(User user, Claim claim);
        Task<IdentityResult> RemoveClaimFromUser(User user, Claim claim);
    }
}
