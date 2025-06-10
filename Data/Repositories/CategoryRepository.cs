using Data.Entities;
using Data.Helpers;
using Data.Helpers;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CategoryRepository(ApplicationDbContext context)
    : GenericRepository<Category>(context), ICategoryRepository
{


    public List<Category> GetCategories()
    {
        List<Category> categories = GetAll(query => query.Include(c => c.CategoryBooks)
                                .ThenInclude(cb => cb.Book)).ToList();
        return categories;


    }
    public async Task<Category> GetCategoryById(string id)
    {
        Category category = await _context.Categories.Include(c => c.CategoryBooks)
            .ThenInclude(cb => cb.Book).FirstOrDefaultAsync(c => c.Id == id);
        return category;
    }

}
