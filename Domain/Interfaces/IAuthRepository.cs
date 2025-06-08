using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Interfaces
{
    public interface IAuthRepository
    {
        IQueryable<User> GetUsersAsync();
        Task<User> GetUserByIdAsync(string id);
        Task<User?> GetByNameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task<IdentityResult> CreateAsync(User user, string password);
        Task<IdentityResult> UpdateAsync(User user);
        Task<IdentityResult> DeleteAsync(User user);
        bool VerifyPasswordAsync(User user, string password);
        Task<IdentityResult> AddToRoleAsync(User user, string role);

    }
}
