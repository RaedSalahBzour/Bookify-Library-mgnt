using AutoMapper;
using Bookify_Library_mgnt.Dtos.Borrowings;
using Bookify_Library_mgnt.Helper.Pagination;
using Bookify_Library_mgnt.Models;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Bookify_Library_mgnt.Services.Interfaces;

namespace Bookify_Library_mgnt.Services.Implementations
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
        public async Task<BorrowingDto> GetBorrowingByIdAsync(string id)
        {
            var borrowing = await _borrowingRepository.GetBorrowingByIdAsync(id);
            var borrowingDto = _mapper.Map<BorrowingDto>(borrowing);
            if (borrowing == null) { return null; }
            return borrowingDto;
        }

        public async Task<Borrowing> CreateBorrowingAsync(CreateBorrowingDto borrowingDto)
        {
            var borrowing = _mapper.Map<Borrowing>(borrowingDto);
            await _borrowingRepository.CreateBorrowingAsync(borrowing);
            await _borrowingRepository.SaveChangesAsync();
            return borrowing;
        }
        public async Task<Borrowing> UpdateBorrowingAsync(string id, UpdateBorrowingDto borrowingDto)
        {
            var borrowing = await _borrowingRepository.GetBorrowingByIdAsync(id);
            if (borrowing == null) { return null; }
            _mapper.Map(borrowingDto, borrowing);
            await _borrowingRepository.UpdateBorrowingAsync(borrowing);
            await _borrowingRepository.SaveChangesAsync();
            return borrowing;
        }

        public async Task<Borrowing> DeleteBorrowingAsync(string id)
        {
            var borrowing = await _borrowingRepository.GetBorrowingByIdAsync(id);
            if (borrowing == null) { return null; }
            await _borrowingRepository.DeleteBorrowingAsync(borrowing);
            await _borrowingRepository.SaveChangesAsync();
            return borrowing;
        }



    }
}
