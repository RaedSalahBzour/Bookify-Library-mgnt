using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Presestence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositpries
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Book> GetBooksAsync()
        {
            var query = _context.Books.AsNoTracking()
                 .Include(b => b.Reviews)
                 .Include(b => b.Borrowings)
                 .Include(b => b.CategoryBooks)
                 .ThenInclude(cb => cb.Category)
                 .AsQueryable();
            return query;
        }

        public async Task<Book> GetByIdAsync(string id)
        {
            return await _context.Books.Include(b => b.CategoryBooks).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Book> CreateBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            return book;
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            return book;
        }

        public async Task<Book> DeleteBookAsync(Book book)
        {
            _context.Books.Remove(book);
            return book;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsCategoryExist(string id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }
    }
}
