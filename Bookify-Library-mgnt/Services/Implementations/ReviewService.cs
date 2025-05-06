using AutoMapper;
using Bookify_Library_mgnt.Dtos.Reviews;
using Bookify_Library_mgnt.Helper.Pagination;
using Bookify_Library_mgnt.Models;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Bookify_Library_mgnt.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace Bookify_Library_mgnt.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        public ReviewService(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }
        public async Task<PagedResult<ReviewDto>> GetReviewsAsync(int pageNumber = 1, int pageSize = 10)
        {
            var queryReviews = _reviewRepository.GetReviewsAsync();
            var paginatedReview = await queryReviews.ToPaginationForm(pageNumber, pageSize);

            var reviewsDto = _mapper.Map<IEnumerable<ReviewDto>>(paginatedReview.Items);
            return new PagedResult<ReviewDto>
            {
                TotalCount = paginatedReview.TotalCount,
                Items = reviewsDto,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };
        }
        public async Task<ReviewDto> GetReviewByIdAsync(string id)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(id);
            if (review is null)
            {
                return null;//it must be return Result.error(review not found) => Generic Result
            }
            var reviewDto = _mapper.Map<ReviewDto>(review);
            return reviewDto;
        }
        public async Task<Review> CreateReviewAsync(CreateReviewDto dto)
        {
            var review = _mapper.Map<Review>(dto);
            var existance = await _reviewRepository.IsUserAndBookExists(dto.UserId, dto.BookId);
            if (existance is false)
            {
                return null;//it must be return Result.error(review not found) => Generic Result
            }
            await _reviewRepository.CreateReviewAsync(review);
            await _reviewRepository.SaveChangesAsync();
            return review;
        }
        public async Task<Review> UpdateReviewAsync(string id, UpdateReviewDto dto)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(id);
            if (review is null)
                return null;//it must be return Result.error(review not found) => Generic Result
            _mapper.Map(dto, review);
            await _reviewRepository.UpdateReviewAsync(review);
            await _reviewRepository.SaveChangesAsync();
            return review;

        }
        public async Task<string> DeleteReviewAsync(string id)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(id);
            if (review is null)
                return null;//it must be return Result.error(review not found) => Generic Result
            await _reviewRepository.DeleteReviewAsync(review);
            await _reviewRepository.SaveChangesAsync();
            return review.Id;
        }


    }
}
