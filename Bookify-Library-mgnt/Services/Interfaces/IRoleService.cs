using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Dtos.Categories;
using Bookify_Library_mgnt.Dtos.Roles;
using Bookify_Library_mgnt.Helper.Pagination;
using Bookify_Library_mgnt.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Bookify_Library_mgnt.Services.Interfaces
{
    public interface IRoleService
    {
        Task<PagedResult<RoleDto>> GetRolesAsync(int pageNumber = 1, int pageSize = 10);
        Task<Result<RoleDto>> GetRoleByIdAsync(string id);
        Task<Result<IdentityRole>> CreateRoleAsync(CreateRoleDto roleDto);
        Task<Result<IdentityRole>> UpdateRoleAsync(string id, UpdateRoleDto roleDto);
        Task<Result<IdentityRole>> DeleteRoleAsync(string id);
        Task<Result<bool>> AddUserToRoleAsync(string userId, string roleId);
        Task<Result<bool>> RemoveUserFromRoleAsync(string userId, string roleName);

    }
}
