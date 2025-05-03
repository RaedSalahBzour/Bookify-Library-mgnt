using Bookify_Library_mgnt.Dtos.Reviews;
using Bookify_Library_mgnt.Models;

namespace Bookify_Library_mgnt.Repositpries.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<ReviewDto>> GetReviewsAsync();
        Task<ReviewDto> GetReviewByIdAsync(string id);
        Task<Review> CreateReviewAsync(CreateReviewDto dto);
        Task<Review> UpdateReviewAsync(string id, UpdateReviewDto dto);
        Task<string> DeleteReviewAsync(string id);
    }
}
