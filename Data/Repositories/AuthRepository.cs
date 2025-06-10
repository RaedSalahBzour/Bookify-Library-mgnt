using Data.Entities;
using Data.Helpers;
using Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Data.Repositories;

public class AuthRepository(ApplicationDbContext context,
    UserManager<User> userManager, IPasswordHasher<User> passwordHasher) : IAuthRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly UserManager<User> _userManager = userManager;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;


    public List<User> GetUsersAsync()
    {
        List<User> users = _context.Users
            .Include(u => u.Borrowings)
            .Include(u => u.Reviews).ToList();

        return users;
    }
    public async Task<User?> GetUserByIdAsync(string id)
    {
        User? user = await _context.Users.Include(u => u.Borrowings)
            .Include(u => u.Reviews)
            .FirstOrDefaultAsync(x => x.Id == id);
        return user;
    }

    public async Task<User?> GetUserByNameAsync(string username)
    {
        User? user = await _userManager.FindByNameAsync(username);
        return user;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        User? user = await _userManager.FindByEmailAsync(email);
        return user;
    }

    public async Task<IdentityResult> CreateAsync(User user, string password)
    {
        IdentityResult identityResult = await _userManager.CreateAsync(user, password);
        return identityResult;
    }

    public async Task<IdentityResult> UpdateAsync(User user)
    {
        IdentityResult identityResult = await _userManager.UpdateAsync(user);
        return identityResult;
    }

    public async Task<IdentityResult> DeleteAsync(User user)
    {
        IdentityResult identityResult = await _userManager.DeleteAsync(user);
        return identityResult;
    }
    public async Task<IList<Claim>> GetUserClaimsAsync(User user)
    {
        IList<Claim> claims = await _userManager.GetClaimsAsync(user);
        return claims;
    }

    public async Task<IdentityResult> AddToRoleAsync(User user, string role)
    {
        IdentityResult identityResult = await _userManager.AddToRoleAsync(user, role);
        return identityResult;
    }

    public bool VerifyPasswordAsync(User user, string password)
    {
        PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        return result != PasswordVerificationResult.Failed;
    }


}

