using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Data.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public RoleRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IdentityResult> AddUserToRoleAsync(User user, string roleName)
    {
        return await _userManager.AddToRoleAsync(user, roleName);
    }

    public async Task<IdentityResult> CreateRoleAsync(IdentityRole role)
    {
        return await _roleManager.CreateAsync(role);
    }

    public async Task<IdentityResult> DeleteRoleAsync(IdentityRole role)
    {
        return await _roleManager.DeleteAsync(role);
    }

    public async Task<IdentityRole?> FindRoleByIdAsync(string id)
    {
        return await _roleManager.FindByIdAsync(id);
    }

    public async Task<IdentityRole?> FindRoleByNameAsync(string name)
    {
        return await _roleManager.FindByNameAsync(name);
    }

    public async Task<IList<Claim>?> GetRoleClaimsAsync(IdentityRole role)
    {
        return await _roleManager.GetClaimsAsync(role);
    }

    public List<IdentityRole> GetRoles()
    {
        return _roleManager.Roles.ToList();
    }

    public async Task<IList<string>?> GetUserRolesAsync(User user)
    {
        return await _userManager.GetRolesAsync(user);
    }

    public async Task<IdentityResult> RemoveUserFromRoleAsync(User user, string roleName)
    {
        return await _userManager.RemoveFromRoleAsync(user, roleName);
    }

    public async Task<bool> RoleExistsAsync(string roleName)
    {
        return await _roleManager.RoleExistsAsync(roleName);
    }

    public async Task<IdentityResult> UpdateRoleAsync(IdentityRole role)
    {
        return await _roleManager.UpdateAsync(role);
    }
}
