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
        Task<Result<Borrowing>> CreateBorrowingAsync(CreateBorrowingDto borrowing);
        Task<Result<Borrowing>> UpdateBorrowingAsync(string id, UpdateBorrowingDto borrowing);
        Task<Result<Borrowing>> DeleteBorrowingAsync(string id);
    }
}
