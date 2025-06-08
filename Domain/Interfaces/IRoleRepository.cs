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
    public interface IRoleRepository
    {
        Task<IdentityResult> AddUserToRoleAsync(User user, string roleName);
        Task<IdentityResult> RemoveUserFromRoleAsync(User user, string roleName);
        IQueryable<IdentityRole> GetRoles();
        Task<IdentityRole?> FindRoleByNameAsync(string name);
        Task<IdentityRole?> FindRoleByIdAsync(string id);
        Task<IdentityResult> CreateRoleAsync(IdentityRole role);
        Task<IdentityResult> UpdateRoleAsync(IdentityRole role);
        Task<IdentityResult> DeleteRoleAsync(IdentityRole role);
        Task<IList<string>?> GetUserRolesAsync(User user);
        Task<IList<Claim>?> GetRoleClaimsAsync(IdentityRole role);
        Task<bool> RoleExistsAsync(string roleName);
    }
}
