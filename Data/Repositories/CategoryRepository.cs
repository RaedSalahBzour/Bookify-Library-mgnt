using Data.Entities;
using Data.Helpers;
using Data.Helpers;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
    }


    public List<Category> GetCategories()
    {
        return GetAll(query => query.Include(c => c.CategoryBooks)
                                .ThenInclude(cb => cb.Book)).ToList();


    }
    public async Task<Category> GetCategoryById(string id)
    {
        return await _context.Categories.Include(c => c.CategoryBooks).ThenInclude(cb => cb.Book).FirstOrDefaultAsync(c => c.Id == id);
    }

}
