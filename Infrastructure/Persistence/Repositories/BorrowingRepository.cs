using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Presestence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositpries
{
    public class BorrowingRepository : IBorrowingRepository
    {
        private readonly ApplicationDbContext _context;
        public BorrowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<Borrowing> GetBorrowingsAsync()
        {
            return _context.Borrowings.AsQueryable();
        }
        public async Task<Borrowing> GetBorrowingByIdAsync(string id)
        {
            return await _context.Borrowings.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Borrowing> CreateBorrowingAsync(Borrowing borrowing)
        {
            await _context.Borrowings.AddAsync(borrowing);
            return borrowing;
        }

        public async Task<Borrowing> UpdateBorrowingAsync(Borrowing borrowing)
        {
            _context.Borrowings.Update(borrowing);
            return borrowing;
        }
        public async Task<Borrowing> DeleteBorrowingAsync(Borrowing borrowing)
        {
            _context.Borrowings.Remove(borrowing);
            return borrowing;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<(bool userExists, bool bookExists)> CheckUserAndBookExistAsync(string userId,
            string bookId)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
            var bookExists = await _context.Books.AnyAsync(b => b.Id == bookId);
            return (userExists, bookExists);
        }
    }


}
