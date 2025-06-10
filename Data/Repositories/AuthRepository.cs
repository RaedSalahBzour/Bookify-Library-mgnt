using Data.Entities;
using Data.Helpers;
using Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Data.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IPasswordHasher<User> _passwordHasher;
    public AuthRepository(ApplicationDbContext context, UserManager<User> userManager, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _userManager = userManager;
        _passwordHasher = passwordHasher;
    }

    public List<User> GetUsersAsync()
    {
        var query = _context.Users
            .Include(u => u.Borrowings)
            .Include(u => u.Reviews).ToList();

        return query;
    }
    public async Task<User?> GetUserByIdAsync(string id)
    {
        var user = await _context.Users.Include(u => u.Borrowings)
            .Include(u => u.Reviews)
            .FirstOrDefaultAsync(x => x.Id == id);
        return user;
    }

    public async Task<User?> GetUserByNameAsync(string username)
    {
        return await _userManager.FindByNameAsync(username);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<IdentityResult> CreateAsync(User user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<IdentityResult> UpdateAsync(User user)
    {
        return await _userManager.UpdateAsync(user);
    }

    public async Task<IdentityResult> DeleteAsync(User user)
    {
        return await _userManager.DeleteAsync(user);
    }
    public async Task<IList<Claim>> GetUserClaimsAsync(User user)
    {
        return await _userManager.GetClaimsAsync(user);
    }

    public async Task<IdentityResult> AddToRoleAsync(User user, string role)
    {
        return await _userManager.AddToRoleAsync(user, role);
    }

    public bool VerifyPasswordAsync(User user, string password)
    {
        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        return result != PasswordVerificationResult.Failed;
    }


}

