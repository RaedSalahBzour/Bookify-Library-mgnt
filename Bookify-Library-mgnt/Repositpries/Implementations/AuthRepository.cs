using AutoMapper;
using Bookify_Library_mgnt.Data;
using Bookify_Library_mgnt.Dtos.Users;
using Bookify_Library_mgnt.Helper.Pagination;
using Bookify_Library_mgnt.Models;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bookify_Library_mgnt.Repositpries.Implementations
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
