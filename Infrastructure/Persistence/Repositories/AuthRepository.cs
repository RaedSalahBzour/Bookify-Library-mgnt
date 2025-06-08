using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositpries
{
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

        public IQueryable<User> GetUsersAsync()
        {
            var query = _context.Users
                .Include(u => u.Borrowings)
                .Include(u => u.Reviews)
                .AsQueryable();

            return query;
        }
        public async Task<User> GetUserByIdAsync(string id)
        {
            var user = await _context.Users.Include(u => u.Borrowings)
                .Include(u => u.Reviews)
                .FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public Task<User?> GetByNameAsync(string username)
        {
            return _userManager.FindByNameAsync(username);
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }

        public Task<IdentityResult> CreateAsync(User user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

        public Task<IdentityResult> UpdateAsync(User user)
        {
            return _userManager.UpdateAsync(user);
        }

        public Task<IdentityResult> DeleteAsync(User user)
        {
            return _userManager.DeleteAsync(user);
        }


        public Task<IdentityResult> AddToRoleAsync(User user, string role)
        {
            return _userManager.AddToRoleAsync(user, role);
        }

        public bool VerifyPasswordAsync(User user, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result != PasswordVerificationResult.Failed;
        }
    }
}
