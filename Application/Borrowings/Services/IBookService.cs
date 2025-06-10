using Application.Borrowings.Dtos;

namespace Application.Borrowings.Services;

public interface IBorrowingService
{
    Task<List<BorrowingDto>> GetBorrowingsAsync();
    Task<BorrowingDto> GetBorrowingByIdAsync(string id);
    Task<BorrowingDto> CreateBorrowingAsync(CreateBorrowingDto borrowing);
    Task<BorrowingDto> UpdateBorrowingAsync(string id, UpdateBorrowingDto borrowing);
    Task<BorrowingDto> DeleteBorrowingAsync(string id);
}
