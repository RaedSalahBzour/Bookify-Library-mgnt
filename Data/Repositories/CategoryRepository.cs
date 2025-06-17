using Data.Entities;
using Data.Helpers;
using Data.Helpers;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Data.Repositories;

public class CategoryRepository(ApplicationDbContext context)
    : GenericRepository<Category>(context), ICategoryRepository
{


    public async Task<List<Category>> GetCategories()
    {
        List<Category> categories = await GetAll(query => query.Include(c => c.CategoryBooks)
                                .ThenInclude(cb => cb.Book));
        return categories;


    }
    public async Task<Category> GetCategoryById(string id)
    {
        Category category = await _context.Categories.Include(c => c.CategoryBooks)
            .ThenInclude(cb => cb.Book).FirstOrDefaultAsync(c => c.Id == id);
        return category;
    }
    public async Task<bool> ExistsByNameAsync(string name) =>
    await _context.Categories.AnyAsync(c => c.Name == name);
}
