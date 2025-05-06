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
        private readonly IMapper _mapper;
        public ReviewRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IQueryable<Review>> GetReviewsAsync()
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
        public async Task<Review> UpdateReviewAsync(string id, Review review)
        {
            _context.Reviews.Update(review);
            return review;
        }

        public async Task<Review> DeleteReviewAsync(string id)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id);
            if (review is null)
            {
                return null;
            }
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsUserAndBookExists(string userId, string bookId)
        {
            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == bookId);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null && book == null) return false;
            return true;
        }

    }
}
