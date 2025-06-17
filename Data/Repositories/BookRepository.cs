using Data.Entities;
using Data.Helpers;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class BookRepository(ApplicationDbContext context)
    : GenericRepository<Book>(context), IBookRepository
{



    public async Task<List<Book>> GetBooksAsync()
    {
        List<Book> books = await GetAll(query =>
        query.Include(b => b.Reviews)
         .Include(b => b.Borrowings)
         .Include(b => b.CategoryBooks)
         .ThenInclude(cb => cb.Category));
        return books;
    }

    public async Task<Book> GetBookByIdAsync(string id)
    {
        Book book = await _context.Books.Include(b => b.Reviews)
         .Include(b => b.Borrowings)
         .Include(b => b.CategoryBooks)
         .ThenInclude(cb => cb.Category).FirstOrDefaultAsync(x => x.Id == id);
        return book;
    }

    public async Task<bool> IsCategoryExist(string id)
    {
        bool isExist = await _context.Categories.AnyAsync(c => c.Id == id);
        return isExist;
    }
}

