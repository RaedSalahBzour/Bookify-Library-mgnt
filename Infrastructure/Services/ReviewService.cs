using Application.Common.Interfaces;
using Application.Reviews.Dtos;
using Application.Reviews.Services;
using AutoMapper;
using Domain.Entities;
namespace Infrastructure.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReviewService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<ReviewDto>> GetReviewsAsync()
        {
            var reviews = _unitOfWork.ReviewRepository.GetAll();

            return _mapper.Map<List<ReviewDto>>(reviews);

        }
        public async Task<ReviewDto> GetReviewByIdAsync(string id)
        {
            var review = await _unitOfWork.ReviewRepository.GetByIdAsync(id);
            if (review is null)
                throw new KeyNotFoundException($"Review With Id {id} Was Not Found");
            return _mapper.Map<ReviewDto>(review);

        }
        public async Task<ReviewDto> CreateReviewAsync(CreateReviewDto dto)
        {

            var review = _mapper.Map<Review>(dto);
            var (userExists, bookExists) = await _unitOfWork.ReviewRepository.CheckUserAndBookExistAsync(dto.UserId, dto.BookId);

            if (!userExists)
                throw new KeyNotFoundException($"User With Id {dto.UserId} Was Not Found");

            if (!bookExists)
                throw new KeyNotFoundException($"Book With Id {dto.BookId} Was Not Found");

            await _unitOfWork.ReviewRepository.AddAsync(review);
            await _unitOfWork.ReviewRepository.SaveChangesAsync();
            return _mapper.Map<ReviewDto>(review);

        }
        public async Task<ReviewDto> UpdateReviewAsync(string id, UpdateReviewDto dto)
        {
            var review = await _unitOfWork.ReviewRepository.GetByIdAsync(id);
            if (review is null)
                throw new KeyNotFoundException($"Review With Id {id} Was Not Found");
            var (userExists, bookExists) =
                await _unitOfWork.ReviewRepository
                .CheckUserAndBookExistAsync(dto.UserId, dto.BookId);

            if (!userExists)
                throw new KeyNotFoundException($"User With Id {dto.UserId} Was Not Found");

            if (!bookExists)
                throw new KeyNotFoundException($"Book With Id {dto.BookId} Was Not Found");

            _mapper.Map(dto, review);
            await _unitOfWork.ReviewRepository.Update(review);
            await _unitOfWork.ReviewRepository.SaveChangesAsync();
            return _mapper.Map<ReviewDto>(review);


        }
        public async Task<ReviewDto> DeleteReviewAsync(string id)
        {
            var review = await _unitOfWork.ReviewRepository.GetByIdAsync(id);
            if (review is null)
                throw new KeyNotFoundException($"Review With Id {id} Was Not Found");
            await _unitOfWork.ReviewRepository.Delete(review);
            await _unitOfWork.ReviewRepository.SaveChangesAsync();
            return _mapper.Map<ReviewDto>(review);

        }


    }
}
