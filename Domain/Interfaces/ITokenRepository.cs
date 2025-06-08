using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITokenRepository
    {
        Task<IdentityResult> UpdateUserAsync(User user);
        Task<User> FindUserByIdAsync(string id);
        Task<IList<Claim>> GetUserClaimsAsync(User user);
        Task<IList<string>> GetUserRolesAsync(User user);
        Task<IList<Claim>> GetRoleClaimsAsync(IdentityRole role);
        Task<IdentityRole> FindRoleByNameAsync(string name);
    }
}
