using Application.Borrowings.Dtos;
using Application.Borrowings.Services;
using Application.Common.Interfaces;
using Application.Reviews.Dtos;
using Application.Users.Dtos;
using AutoMapper;
using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Helper.Pagination;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Shared;
using Infrastructure.Persistence.Repositpries;
namespace Infrastructure.Services
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BorrowingService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<BorrowingDto>> GetBorrowingsAsync(int pageNumber = 1, int pageSize = 10)
        {
            var borrowings = _unitOfWork.BorrowingRepository.GetAll();
            var paginatedBorrowings = await borrowings.ToPaginationForm(pageNumber, pageSize);
            var borrowingsDto = _mapper.Map<IEnumerable<BorrowingDto>>(paginatedBorrowings.Items);
            return new PagedResult<BorrowingDto>
            {
                TotalCount = paginatedBorrowings.TotalCount,
                Items = borrowingsDto,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        public async Task<Result<BorrowingDto>> GetBorrowingByIdAsync(string id)
        {
            var borrowing = await _unitOfWork.BorrowingRepository.GetByIdAsync(id);
            var borrowingDto = _mapper.Map<BorrowingDto>(borrowing);
            if (borrowing == null) { return Result<BorrowingDto>.Fail(ErrorMessages.NotFoundById(id)); }
            return Result<BorrowingDto>.Ok(borrowingDto);
        }

        public async Task<Result<BorrowingDto>> CreateBorrowingAsync(CreateBorrowingDto borrowingDto)
        {
            var borrowing = _mapper.Map<Borrowing>(borrowingDto);
            var bookExist = await _unitOfWork.BookRepository.GetByIdAsync(borrowingDto.BookId);
            var userExist = await _unitOfWork.AuthRepository.GetUserByIdAsync(borrowingDto.UserId);
            if (bookExist is null)
            {
                return Result<BorrowingDto>.Fail(ErrorMessages.NotFoundById(borrowingDto.BookId));
            }
            if (userExist is null)
            {
                return Result<BorrowingDto>.Fail(ErrorMessages.NotFoundById(borrowingDto.UserId));
            }
            await _unitOfWork.BorrowingRepository.AddAsync(borrowing);
            await _unitOfWork.BorrowingRepository.SaveChangesAsync();
            var bDto = _mapper.Map<BorrowingDto>(borrowing);
            return Result<BorrowingDto>.Ok(bDto);
        }
        public async Task<Result<BorrowingDto>> UpdateBorrowingAsync(string id, UpdateBorrowingDto borrowingDto)
        {
            var borrowing = await _unitOfWork.BorrowingRepository.GetByIdAsync(id);
            if (borrowing == null) { Result<BorrowingDto>.Fail(ErrorMessages.NotFoundById(id)); }
            _mapper.Map(borrowingDto, borrowing);
            var (userExists, bookExists) = await _unitOfWork.BorrowingRepository.CheckUserAndBookExistAsync(borrowingDto.UserId, borrowingDto.BookId);

            if (!userExists || !bookExists)
            {
                var errors = new List<string>();

                if (!userExists)
                    errors.Add($"User with ID {borrowingDto.UserId} not found");

                if (!bookExists)
                    errors.Add($"Book with ID {borrowingDto.BookId} not found");

                return Result<BorrowingDto>.Fail(string.Join(" | ", errors));
            }
            await _unitOfWork.BorrowingRepository.Update(borrowing);
            await _unitOfWork.BorrowingRepository.SaveChangesAsync();
            var bDto = _mapper.Map<BorrowingDto>(borrowing);
            return Result<BorrowingDto>.Ok(bDto);
        }

        public async Task<Result<BorrowingDto>> DeleteBorrowingAsync(string id)
        {
            var borrowing = await _unitOfWork.BorrowingRepository.GetByIdAsync(id);
            if (borrowing == null) { return Result<BorrowingDto>.Fail(ErrorMessages.NotFoundById(id)); }
            await _unitOfWork.BorrowingRepository.Delete(borrowing);
            await _unitOfWork.BorrowingRepository.SaveChangesAsync();
            var bDto = _mapper.Map<BorrowingDto>(borrowing);
            return Result<BorrowingDto>.Ok(bDto);
        }



    }
}