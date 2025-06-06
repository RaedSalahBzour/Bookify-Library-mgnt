using Application.Reviews.Dtos;
using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Helper.Pagination;
using Domain.Entities;

namespace Application.Reviews.Services
{
    public interface IReviewService
    {
        Task<PagedResult<ReviewDto>> GetReviewsAsync(int pageNumber = 1, int pageSize = 10);
        Task<Result<ReviewDto>> GetReviewByIdAsync(string id);
        Task<Result<ReviewDto>> CreateReviewAsync(CreateReviewDto dto);
        Task<Result<ReviewDto>> UpdateReviewAsync(string id, UpdateReviewDto dto);
        Task<Result<ReviewDto>> DeleteReviewAsync(string id);
    }
}
