using Application.Authorization.Dtos.Roles;
using Microsoft.AspNetCore.Identity;

namespace Application.Authorization.Services;

public interface IRoleService
{
    Task<List<RoleDto>> GetRolesAsync();
    Task<RoleDto> GetRoleByIdAsync(string id);
    Task<IdentityRole> CreateRoleAsync(CreateRoleDto roleDto);
    Task<IdentityRole> UpdateRoleAsync(string id, UpdateRoleDto roleDto);
    Task<IdentityRole> DeleteRoleAsync(string id);
    Task<string> AddUserToRoleAsync(string userId, string roleId);
    Task<string> RemoveUserFromRoleAsync(string userId, string roleName);
    Task<IList<string>> GetUserRoles(string email);

}
