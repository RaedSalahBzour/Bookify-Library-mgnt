using Application.Borrowings.Dtos;
using Application.Borrowings.Services;
using Application.Users.Dtos;
using AutoMapper;
using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Helper.Pagination;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Shared;
namespace Infrastructure.Services
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IBorrowingRepository _borrowingRepository;
        private readonly IMapper _mapper;
        public BorrowingService(IBorrowingRepository borrowingRepository, IMapper mapper)
        {
            _borrowingRepository = borrowingRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<BorrowingDto>> GetBorrowingsAsync(int pageNumber = 1, int pageSize = 10)
        {
            var borrowings = _borrowingRepository.GetBorrowingsAsync();
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
            var borrowing = await _borrowingRepository.GetBorrowingByIdAsync(id);
            var borrowingDto = _mapper.Map<BorrowingDto>(borrowing);
            if (borrowing == null) { return Result<BorrowingDto>.Fail(ErrorMessages.NotFoundById(id)); }
            return Result<BorrowingDto>.Ok(borrowingDto);
        }

        public async Task<Result<BorrowingDto>> CreateBorrowingAsync(CreateBorrowingDto borrowingDto)
        {
            var borrowing = _mapper.Map<Borrowing>(borrowingDto);
            await _borrowingRepository.CreateBorrowingAsync(borrowing);
            await _borrowingRepository.SaveChangesAsync();
            var bDto = _mapper.Map<BorrowingDto>(borrowing);
            return Result<BorrowingDto>.Ok(bDto);
        }
        public async Task<Result<BorrowingDto>> UpdateBorrowingAsync(string id, UpdateBorrowingDto borrowingDto)
        {
            var borrowing = await _borrowingRepository.GetBorrowingByIdAsync(id);
            if (borrowing == null) { Result<BorrowingDto>.Fail(ErrorMessages.NotFoundById(id)); }
            _mapper.Map(borrowingDto, borrowing);
            await _borrowingRepository.UpdateBorrowingAsync(borrowing);
            await _borrowingRepository.SaveChangesAsync();
            var bDto = _mapper.Map<BorrowingDto>(borrowing);
            return Result<BorrowingDto>.Ok(bDto);
        }

        public async Task<Result<BorrowingDto>> DeleteBorrowingAsync(string id)
        {
            var borrowing = await _borrowingRepository.GetBorrowingByIdAsync(id);
            if (borrowing == null) { return Result<BorrowingDto>.Fail(ErrorMessages.NotFoundById(id)); }
            await _borrowingRepository.DeleteBorrowingAsync(borrowing);
            await _borrowingRepository.SaveChangesAsync();
            var bDto = _mapper.Map<BorrowingDto>(borrowing);
            return Result<BorrowingDto>.Ok(bDto);
        }



    }
}