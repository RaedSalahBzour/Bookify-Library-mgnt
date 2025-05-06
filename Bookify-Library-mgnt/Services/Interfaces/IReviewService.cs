using Bookify_Library_mgnt.Dtos.Reviews;
using Bookify_Library_mgnt.Helper.Pagination;
using Bookify_Library_mgnt.Models;

namespace Bookify_Library_mgnt.Services.Interfaces
{
    public interface IReviewService
    {
        Task<PagedResult<ReviewDto>> GetReviewsAsync(int pageNumber = 1, int pageSize = 10);
        Task<ReviewDto> GetReviewByIdAsync(string id);
        Task<Review> CreateReviewAsync(CreateReviewDto dto);
        Task<Review> UpdateReviewAsync(string id, UpdateReviewDto dto);
        Task<string> DeleteReviewAsync(string id);
    }
}
