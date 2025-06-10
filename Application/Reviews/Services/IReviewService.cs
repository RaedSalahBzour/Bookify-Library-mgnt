using Application.Reviews.Dtos;

namespace Application.Reviews.Services;

public interface IReviewService
{
    Task<List<ReviewDto>> GetReviewsAsync();
    Task<ReviewDto> GetReviewByIdAsync(string id);
    Task<ReviewDto> CreateReviewAsync(CreateReviewDto dto);
    Task<ReviewDto> UpdateReviewAsync(string id, UpdateReviewDto dto);
    Task<ReviewDto> DeleteReviewAsync(string id);
}
