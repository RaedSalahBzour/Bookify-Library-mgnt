using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositpries
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext context) : base(context)
        {
        }


        public async Task<(bool userExists, bool bookExists)> CheckUserAndBookExistAsync(string userId,
            string bookId)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
            var bookExists = await _context.Books.AnyAsync(b => b.Id == bookId);
            return (userExists, bookExists);
        }

    }
}
