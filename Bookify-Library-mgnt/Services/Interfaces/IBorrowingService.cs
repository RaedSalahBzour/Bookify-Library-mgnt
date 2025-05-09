using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Dtos.Borrowings;
using Bookify_Library_mgnt.Helper.Pagination;
using Bookify_Library_mgnt.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bookify_Library_mgnt.Services.Interfaces
{
    public interface IBorrowingService
    {
        Task<PagedResult<BorrowingDto>> GetBorrowingsAsync(int pageNumber = 1, int pageSize = 10);
        Task<Result<BorrowingDto>> GetBorrowingByIdAsync(string id);
        Task<Result<Borrowing>> CreateBorrowingAsync(CreateBorrowingDto borrowing);
        Task<Result<Borrowing>> UpdateBorrowingAsync(string id, UpdateBorrowingDto borrowing);
        Task<Result<Borrowing>> DeleteBorrowingAsync(string id);
    }
}
