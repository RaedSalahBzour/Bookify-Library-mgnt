using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositpries
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;
        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
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

    }
}
