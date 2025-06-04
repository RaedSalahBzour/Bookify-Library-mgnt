using Application.Authorization.Dtos.Roles;
using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Helper.Pagination;
using Microsoft.AspNetCore.Identity;

namespace Application.Authorization.Services
{
    public interface IRoleService
    {
        Task<PagedResult<RoleDto>> GetRolesAsync(int pageNumber = 1, int pageSize = 10);
        Task<Result<RoleDto>> GetRoleByIdAsync(string id);
        Task<Result<IdentityRole>> CreateRoleAsync(CreateRoleDto roleDto);
        Task<Result<IdentityRole>> UpdateRoleAsync(string id, UpdateRoleDto roleDto);
        Task<Result<IdentityRole>> DeleteRoleAsync(string id);
        Task<Result<string>> AddUserToRoleAsync(string userId, string roleId);
        Task<Result<string>> RemoveUserFromRoleAsync(string userId, string roleName);
        Task<Result<IEnumerable<string>>> GetUserRoles(string email);

    }
}
