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
    public class TokenRepository : ITokenRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TokenRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public Task<IdentityRole> FindRoleByNameAsync(string name)
        {
            return _roleManager.FindByNameAsync(name);
        }
        public async Task<IList<Claim>> GetRoleClaimsAsync(IdentityRole role)
        {
            return await _roleManager.GetClaimsAsync(role);
        }

        public Task<User> FindUserByIdAsync(string id)
        {
            return _userManager.FindByIdAsync(id);
        }


        public async Task<IList<Claim>> GetUserClaimsAsync(User user)
        {
            return await _userManager.GetClaimsAsync(user);
        }

        public async Task<IList<string>> GetUserRolesAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public Task<IdentityResult> UpdateUserAsync(User user)
        {
            return _userManager.UpdateAsync(user);
        }
    }
}
