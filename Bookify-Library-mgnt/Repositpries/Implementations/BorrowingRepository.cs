using AutoMapper;
using Bookify_Library_mgnt.Data;
using Bookify_Library_mgnt.Dtos.Borrowings;
using Bookify_Library_mgnt.Dtos.Users;
using Bookify_Library_mgnt.Helper.Pagination;
using Bookify_Library_mgnt.Models;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace Bookify_Library_mgnt.Repositpries.Implementations
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
    }


}
