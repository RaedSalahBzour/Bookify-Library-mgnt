using Application.Books.Dtos;
using Application.Borrowings.Dtos;
using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Helper.Pagination;
using Domain.Entities;

namespace Application.Borrowings.Services
{
    public interface IBorrowingService
    {
        Task<PagedResult<BorrowingDto>> GetBorrowingsAsync(int pageNumber = 1, int pageSize = 10);
        Task<Result<BorrowingDto>> GetBorrowingByIdAsync(string id);
        Task<Result<BorrowingDto>> CreateBorrowingAsync(CreateBorrowingDto borrowing);
        Task<Result<BorrowingDto>> UpdateBorrowingAsync(string id, UpdateBorrowingDto borrowing);
        Task<Result<BorrowingDto>> DeleteBorrowingAsync(string id);
    }
}
