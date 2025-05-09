using AutoMapper;
using Bookify_Library_mgnt.Data;
using Bookify_Library_mgnt.Dtos.Reviews;
using Bookify_Library_mgnt.Helper.Pagination;
using Bookify_Library_mgnt.Models;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Bookify_Library_mgnt.Repositpries.Implementations
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;
        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Review> GetReviewsAsync()
        {
            return _context.Reviews.AsQueryable();
        }
        public async Task<Review> GetReviewByIdAsync(string id)
        {
            return await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Review> CreateReviewAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            return review;
        }
        public async Task<Review> UpdateReviewAsync(Review review)
        {
            _context.Reviews.Update(review);
            return review;
        }

        public async Task<Review> DeleteReviewAsync(Review review)
        {
            _context.Reviews.Remove(review);
            return review;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
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
