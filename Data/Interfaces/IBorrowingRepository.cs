
using Data.Entities;

namespace Data.Interfaces;

public interface IBorrowingRepository : IGenericRepository<Borrowing>
{
    Task<(bool userExists, bool bookExists)> CheckUserAndBookExistAsync(string userId, string bookId);
}
