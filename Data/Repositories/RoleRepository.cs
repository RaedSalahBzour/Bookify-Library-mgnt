using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Data.Repositories;

public class RoleRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    : IRoleRepository
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    public async Task<IdentityResult> AddUserToRoleAsync(User user, string roleName)
    {
        IdentityResult identityResult = await _userManager.AddToRoleAsync(user, roleName);
        return identityResult;
    }

    public async Task<IdentityResult> CreateRoleAsync(IdentityRole role)
    {
        IdentityResult identityResult = await _roleManager.CreateAsync(role);
        return identityResult;
    }

    public async Task<IdentityResult> DeleteRoleAsync(IdentityRole role)
    {
        IdentityResult identityResult = await _roleManager.DeleteAsync(role);
        return identityResult;
    }

    public async Task<IdentityRole?> FindRoleByIdAsync(string id)
    {
        IdentityRole? identityRole = await _roleManager.FindByIdAsync(id);
        return identityRole;
    }

    public async Task<IdentityRole?> FindRoleByNameAsync(string name)
    {
        IdentityRole? identityRole = await _roleManager.FindByNameAsync(name);
        return identityRole;
    }

    public async Task<IList<Claim>?> GetRoleClaimsAsync(IdentityRole role)
    {
        IList<Claim>? claims = await _roleManager.GetClaimsAsync(role);
        return claims;
    }

    public List<IdentityRole> GetRoles()
    {
        List<IdentityRole> identityRoles = _roleManager.Roles.ToList();
        return identityRoles;
    }

    public async Task<IList<string>?> GetUserRolesAsync(User user)
    {
        IList<string>? roles = await _userManager.GetRolesAsync(user);
        return roles;
    }

    public async Task<IdentityResult> RemoveUserFromRoleAsync(User user, string roleName)
    {
        IdentityResult identityResult = await _userManager.RemoveFromRoleAsync(user, roleName);
        return identityResult;
    }

    public async Task<bool> RoleExistsAsync(string roleName)
    {
        bool roleExists = await _roleManager.RoleExistsAsync(roleName);
        return roleExists;
    }

    public async Task<IdentityResult> UpdateRoleAsync(IdentityRole role)
    {
        IdentityResult identityResult = await _roleManager.UpdateAsync(role);
        return identityResult;
    }
}
