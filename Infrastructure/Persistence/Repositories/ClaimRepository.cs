using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly UserManager<User> _userManager;

        public ClaimRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddClaimToUser(User user, Claim claim)
        {
            return await _userManager.AddClaimAsync(user, claim);
        }

        public Task<IdentityResult> RemoveClaimFromUser(User user, Claim claim)
        {
            return _userManager.RemoveClaimAsync(user, claim);
        }
    }
}
