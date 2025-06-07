using Application.Borrowings.Dtos;
using Application.Categories.Dtos;
using Application.Reviews.Dtos;
using Application.Reviews.Services;
using AutoMapper;
using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Helper.Pagination;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Shared;
using FluentValidation;
namespace Infrastructure.Services
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
            var queryReviews = _reviewRepository.GetAll();
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
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review is null)
            {
                return Result<ReviewDto>.Fail(ErrorMessages.NotFoundById(id));
            }
            var reviewDto = _mapper.Map<ReviewDto>(review);
            return Result<ReviewDto>.Ok(reviewDto);
        }
        public async Task<Result<ReviewDto>> CreateReviewAsync(CreateReviewDto dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<ReviewDto>.Fail(errorMessages);
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

                return Result<ReviewDto>.Fail(string.Join(" | ", errors));
            }
            await _reviewRepository.AddAsync(review);
            await _reviewRepository.SaveChangesAsync();
            var rDto = _mapper.Map<ReviewDto>(review);
            return Result<ReviewDto>.Ok(rDto);
        }
        public async Task<Result<ReviewDto>> UpdateReviewAsync(string id, UpdateReviewDto dto)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review is null)
                return Result<ReviewDto>.Fail(ErrorMessages.NotFoundById(id));
            var (userExists, bookExists) = await _reviewRepository.CheckUserAndBookExistAsync(dto.UserId, dto.BookId);

            if (!userExists || !bookExists)
            {
                var errors = new List<string>();

                if (!userExists)
                    errors.Add($"User with ID {dto.UserId} not found");

                if (!bookExists)
                    errors.Add($"Book with ID {dto.BookId} not found");

                return Result<ReviewDto>.Fail(string.Join(" | ", errors));
            }
            _mapper.Map(dto, review);
            await _reviewRepository.Update(review);
            await _reviewRepository.SaveChangesAsync();
            var rDto = _mapper.Map<ReviewDto>(review);
            return Result<ReviewDto>.Ok(rDto);

        }
        public async Task<Result<ReviewDto>> DeleteReviewAsync(string id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review is null)
                return Result<ReviewDto>.Fail(ErrorMessages.NotFoundById(id));
            await _reviewRepository.Delete(review);
            await _reviewRepository.SaveChangesAsync();
            var rDto = _mapper.Map<ReviewDto>(review);
            return Result<ReviewDto>.Ok(rDto);
        }


    }
}
