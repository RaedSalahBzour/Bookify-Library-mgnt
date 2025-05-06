using Bookify_Library_mgnt.Dtos.Borrowings;
using Bookify_Library_mgnt.Helper.Pagination;
using Bookify_Library_mgnt.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bookify_Library_mgnt.Services.Interfaces
{
    public interface IBorrowingService
    {
        Task<PagedResult<BorrowingDto>> GetBorrowingsAsync(int pageNumber = 1, int pageSize = 10);
        Task<BorrowingDto> GetBorrowingByIdAsync(string id);
        Task<Borrowing> CreateBorrowingAsync(CreateBorrowingDto borrowing);
        Task<Borrowing> UpdateBorrowingAsync(string id, UpdateBorrowingDto borrowing);
        Task<Borrowing> DeleteBorrowingAsync(string id);
    }
}
