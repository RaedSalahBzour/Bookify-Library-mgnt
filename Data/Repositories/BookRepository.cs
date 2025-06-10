using Data.Entities;
using Data.Helpers;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class BookRepository : GenericRepository<Book>, IBookRepository
{

    public BookRepository(ApplicationDbContext context) : base(context)
    {
    }

    public List<Book> GetBooksAsync()
    {
        return GetAll(query =>
        query.Include(b => b.Reviews)
         .Include(b => b.Borrowings)
         .Include(b => b.CategoryBooks)
         .ThenInclude(cb => cb.Category)).ToList();
    }

    public async Task<Book> GetBookByIdAsync(string id)
    {
        return await _context.Books.Include(b => b.Reviews)
         .Include(b => b.Borrowings)
         .Include(b => b.CategoryBooks)
         .ThenInclude(cb => cb.Category).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> IsCategoryExist(string id)
    {
        return await _context.Categories.AnyAsync(c => c.Id == id);
    }
}

