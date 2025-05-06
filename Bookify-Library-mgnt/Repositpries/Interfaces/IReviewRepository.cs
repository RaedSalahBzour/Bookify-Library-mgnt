using Bookify_Library_mgnt.Dtos.Reviews;
using Bookify_Library_mgnt.Helper;
using Bookify_Library_mgnt.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookify_Library_mgnt.Repositpries.Interfaces
{
    public interface IReviewRepository
    {
        Task<IQueryable<Review>> GetReviewsAsync();
        Task<Review> GetReviewByIdAsync(string id);
        Task<Review> CreateReviewAsync(Review review);
        Task<Review> UpdateReviewAsync(string id, Review review);
        Task<Review> DeleteReviewAsync(string id);
        Task<bool> IsUserAndBookExists(string userId, string bookId);
        Task SaveChangesAsync();

    }
}
