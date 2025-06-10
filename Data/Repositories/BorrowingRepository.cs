using Data.Entities;
using Data.Helpers;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class BorrowingRepository(ApplicationDbContext context)
    : GenericRepository<Borrowing>(context), IBorrowingRepository
{


    public async Task<(bool userExists, bool bookExists)> CheckUserAndBookExistAsync(string userId,
        string bookId)
    {
        Task<bool> userExists = _context.Users.AnyAsync(u => u.Id == userId);
        Task<bool> bookExists = _context.Books.AnyAsync(b => b.Id == bookId);
        await Task.WhenAll(userExists, bookExists);
        return (userExists.Result, bookExists.Result);
    }
}



