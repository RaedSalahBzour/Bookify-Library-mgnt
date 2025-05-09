using AutoMapper;
using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Dtos.Reviews;
using Bookify_Library_mgnt.Helper.Pagination;
using Bookify_Library_mgnt.Models;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Bookify_Library_mgnt.Services.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace Bookify_Library_mgnt.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateReviewDto> _createValidator;
        private readonly IValidator<UpdateReviewDto> _updateValidator;
        public ReviewService(IReviewRepository reviewRepository, IMapper mapper,
            IValidator<CreateReviewDto> createValidator,
            IValidator<UpdateReviewDto> updateValidator)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
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
        public async Task<Result<ReviewDto>> GetReviewByIdAsync(string id)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(id);
            if (review is null)
            {
                return Result<ReviewDto>.Fail(ErrorMessages.NotFound(id));
            }
            var reviewDto = _mapper.Map<ReviewDto>(review);
            return Result<ReviewDto>.Ok(reviewDto);
        }
        public async Task<Result<Review>> CreateReviewAsync(CreateReviewDto dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<Review>.Fail(errorMessages);
            }

            var review = _mapper.Map<Review>(dto);
            var (userExists, bookExists) = await _reviewRepository.CheckUserAndBookExistAsync(dto.UserId, dto.BookId);

            if (!userExists || !bookExists)
            {
                var errors = new List<string>();

                if (!userExists)
                    errors.Add($"User with ID {dto.UserId} not found");

                if (!bookExists)
                    errors.Add($"Book with ID {dto.BookId} not found");

                return Result<Review>.Fail(string.Join(" | ", errors));
            }
            await _reviewRepository.CreateReviewAsync(review);
            await _reviewRepository.SaveChangesAsync();
            return Result<Review>.Ok(review);
        }
        public async Task<Result<Review>> UpdateReviewAsync(string id, UpdateReviewDto dto)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(id);
            if (review is null)
                return Result<Review>.Fail(ErrorMessages.NotFound(id));
            _mapper.Map(dto, review);
            await _reviewRepository.UpdateReviewAsync(review);
            await _reviewRepository.SaveChangesAsync();
            return Result<Review>.Ok(review);

        }
        public async Task<Result<Review>> DeleteReviewAsync(string id)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(id);
            if (review is null)
                return Result<Review>.Fail(ErrorMessages.NotFound(id));
            await _reviewRepository.DeleteReviewAsync(review);
            await _reviewRepository.SaveChangesAsync();
            return Result<Review>.Ok(review);
        }


    }
}
