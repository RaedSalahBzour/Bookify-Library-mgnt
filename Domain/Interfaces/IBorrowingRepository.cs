
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IBorrowingRepository
    {
        IQueryable<Borrowing> GetBorrowingsAsync();
        Task<Borrowing> GetBorrowingByIdAsync(string id);
        Task<Borrowing> CreateBorrowingAsync(Borrowing borrowing);
        Task<Borrowing> UpdateBorrowingAsync(Borrowing borrowing);
        Task<Borrowing> DeleteBorrowingAsync(Borrowing borrowing);
        Task<(bool userExists, bool bookExists)> CheckUserAndBookExistAsync(string userId, string bookId);

        Task SaveChangesAsync();
    }
}
