using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositpries
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {

        public BookRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<Book> GetBooksAsync()
        {
            return GetAll(query =>
            query.Include(b => b.Reviews)
             .Include(b => b.Borrowings)
             .Include(b => b.CategoryBooks)
             .ThenInclude(cb => cb.Category)
    );
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
}
