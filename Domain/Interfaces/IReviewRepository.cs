using Domain.Entities;


namespace Bookify_Library_mgnt.Repositpries.Interfaces
{
    public interface IReviewRepository
    {
        IQueryable<Review> GetReviewsAsync();
        Task<Review> GetReviewByIdAsync(string id);
        Task<Review> CreateReviewAsync(Review review);
        Task<Review> UpdateReviewAsync(Review review);
        Task<Review> DeleteReviewAsync(Review review);
        Task<(bool userExists, bool bookExists)> CheckUserAndBookExistAsync(string userId, string bookId);
        Task SaveChangesAsync();

    }
}
