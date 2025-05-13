using Bookify_Library_mgnt.Data;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bookify_Library_mgnt.Repositpries.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<IdentityRole> GetRoles()
        {
            return _context.Roles.AsQueryable();
        }
        public async Task<IdentityRole> GetRoleById(string id)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<IdentityRole> CreateRoleAsync(IdentityRole role)
        {
            await _context.Roles.AddAsync(role);
            return role;
        }
        public async Task<IdentityRole> UpdateRoleAsync(IdentityRole role)
        {
            _context.Roles.Update(role);
            return role;
        }
        public async Task<IdentityRole> DeleteRoleAsync(IdentityRole role)
        {
            _context.Roles.Remove(role);
            return role;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
