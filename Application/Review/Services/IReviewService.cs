using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Dtos.Reviews;
using Bookify_Library_mgnt.Helper.Pagination;
using Domain.Entities;

namespace Application.Review.Services
{
    public interface IReviewService
    {
        Task<PagedResult<ReviewDto>> GetReviewsAsync(int pageNumber = 1, int pageSize = 10);
        Task<Result<ReviewDto>> GetReviewByIdAsync(string id);
        Task<Result<Review>> CreateReviewAsync(CreateReviewDto dto);
        Task<Result<Review>> UpdateReviewAsync(string id, UpdateReviewDto dto);
        Task<Result<Review>> DeleteReviewAsync(string id);
    }
}
