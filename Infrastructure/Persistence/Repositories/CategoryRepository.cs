using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Data;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositpries
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }


        public IQueryable<Category> GetCategories()
        {
            return GetAll(query => query.Include(c => c.CategoryBooks)
                                    .ThenInclude(cb => cb.Book));


        }
        public async Task<Category> GetCategoryById(string id)
        {
            return await _context.Categories.Include(c => c.CategoryBooks).ThenInclude(cb => cb.Book).FirstOrDefaultAsync(c => c.Id == id);
        }

    }
}
