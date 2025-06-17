using Application.Reviews.Dtos;
using Application.Reviews.Services;
using AutoMapper;
using Data.Entities;
using Data.Interfaces;
using Service.Exceptions;
namespace Service.Services;

public class ReviewService(IMapper mapper, IUnitOfWork unitOfWork) : IReviewService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<List<ReviewDto>> GetReviewsAsync()
    {
        List<Review> reviews = await _unitOfWork.ReviewRepository.GetAll();
        List<ReviewDto> reviewDtos = _mapper.Map<List<ReviewDto>>(reviews);
        return reviewDtos;
    }
    public async Task<ReviewDto> GetReviewByIdAsync(string id)
    {
        Review? review = await _unitOfWork.ReviewRepository.GetByIdAsync(id);
        if (review is null)
            throw ExceptionManager
                .ReturnNotFound("Review Not Found", $"Review With Id {id} Was Not Found");
        ReviewDto reviewDto = _mapper.Map<ReviewDto>(review);
        return reviewDto;
    }
    public async Task<ReviewDto> CreateReviewAsync(CreateReviewDto dto)
    {

        Review? review = _mapper.Map<Review>(dto);
        (bool userExists, bool bookExists) = await _unitOfWork.ReviewRepository.CheckUserAndBookExistAsync(dto.UserId, dto.BookId);

        if (!userExists)
            throw ExceptionManager
                   .ReturnNotFound("User Not Found", $"User With Id {dto.UserId} Was Not Found");

        if (!bookExists)
            throw ExceptionManager
                    .ReturnNotFound("Book Not Found", $"Book With Id {dto.BookId} Was Not Found");

        await _unitOfWork.ReviewRepository.AddAsync(review);
        await _unitOfWork.ReviewRepository.SaveChangesAsync();
        ReviewDto reviewDto = _mapper.Map<ReviewDto>(review);
        return reviewDto;
    }
    public async Task<ReviewDto> UpdateReviewAsync(string id, UpdateReviewDto dto)
    {
        Review? review = await _unitOfWork.ReviewRepository.GetByIdAsync(id);
        if (review is null)
            throw ExceptionManager
    .ReturnNotFound("Review Not Found", $"Review With Id {id} Was Not Found");
        (bool userExists, bool bookExists) =
           await _unitOfWork.ReviewRepository
           .CheckUserAndBookExistAsync(dto.UserId, dto.BookId);

        if (!userExists)
            throw ExceptionManager
                    .ReturnNotFound("User Not Found", $"User With Id {dto.UserId} Was Not Found");

        if (!bookExists)
            throw ExceptionManager
                    .ReturnNotFound("Book Not Found", $"Book With Id {dto.BookId} Was Not Found");

        _mapper.Map(dto, review);
        await _unitOfWork.ReviewRepository.Update(review);
        await _unitOfWork.ReviewRepository.SaveChangesAsync();
        ReviewDto reviewDto = _mapper.Map<ReviewDto>(review);
        return reviewDto;
    }
    public async Task<ReviewDto> DeleteReviewAsync(string id)
    {
        Review? review = await _unitOfWork.ReviewRepository.GetByIdAsync(id);
        if (review is null)
            throw ExceptionManager
    .ReturnNotFound("Review Not Found", $"Review With Id {id} Was Not Found");
        await _unitOfWork.ReviewRepository.Delete(review);
        await _unitOfWork.ReviewRepository.SaveChangesAsync();
        ReviewDto reviewDto = _mapper.Map<ReviewDto>(review);
        return reviewDto;
    }


}
