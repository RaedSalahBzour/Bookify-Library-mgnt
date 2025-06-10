using Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Data.Interfaces;

public interface IAuthRepository
{
    List<User> GetUsersAsync();
    Task<User?> GetUserByIdAsync(string id);
    Task<User?> GetUserByNameAsync(string username);
    Task<User?> GetUserByEmailAsync(string email);
    Task<IdentityResult> CreateAsync(User user, string password);
    Task<IdentityResult> UpdateAsync(User user);
    Task<IdentityResult> DeleteAsync(User user);
    bool VerifyPasswordAsync(User user, string password);
    Task<IdentityResult> AddToRoleAsync(User user, string role);
    Task<IList<Claim>> GetUserClaimsAsync(User user);

}
