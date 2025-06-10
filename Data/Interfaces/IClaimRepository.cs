using Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Data.Interfaces;

public interface IClaimRepository
{
    Task<IdentityResult> AddClaimToUser(User user, Claim claim);
    Task<IdentityResult> RemoveClaimFromUser(User user, Claim claim);
}
