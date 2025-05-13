using Bookify_Library_mgnt.Dtos.Roles;
using Microsoft.AspNetCore.Identity;

namespace Bookify_Library_mgnt.Repositpries.Interfaces
{
    public interface IRoleRepository
    {
        IQueryable<IdentityRole> GetRoles();
        Task<IdentityRole> GetRoleById(string id);
        Task<IdentityRole> CreateRoleAsync(IdentityRole role);
        Task<IdentityRole> UpdateRoleAsync(IdentityRole role);
        Task<IdentityRole> DeleteRoleAsync(IdentityRole role);
        Task SaveChangesAsync();
    }
}
