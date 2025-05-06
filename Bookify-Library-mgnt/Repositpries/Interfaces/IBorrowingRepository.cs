using Bookify_Library_mgnt.Dtos.Borrowings;
using Bookify_Library_mgnt.Helper.Pagination;
using Bookify_Library_mgnt.Models;

namespace Bookify_Library_mgnt.Repositpries.Interfaces
{
    public interface IBorrowingRepository
    {
        Task<PagedResult<BorrowingDto>> GetBorrowingsAsync(int pageNumber = 1, int pageSize = 10);
        Task<BorrowingDto> GetBorrowingByIdAsync(string id);
        Task<Borrowing> CreateBorrowingAsync(CreateBorrowingDto borrowingDto);
        Task<Borrowing> UpdateBorrowingAsync(string id, UpdateBorrowingDto borrowingdto);
        Task<string> DeleteBorrowingAsync(string id);
    }
}
