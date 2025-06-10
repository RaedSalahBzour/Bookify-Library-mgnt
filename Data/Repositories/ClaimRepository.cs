using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories;

public class ClaimRepository(UserManager<User> userManager) : IClaimRepository
{
    private readonly UserManager<User> _userManager = userManager;



    public async Task<IdentityResult> AddClaimToUser(User user, Claim claim)
    {
        IdentityResult identityResult = await _userManager.AddClaimAsync(user, claim);
        return identityResult;
    }

    public async Task<IdentityResult> RemoveClaimFromUser(User user, Claim claim)
    {
        IdentityResult identityResult = await _userManager.RemoveClaimAsync(user, claim);
        return identityResult;

    }
}
