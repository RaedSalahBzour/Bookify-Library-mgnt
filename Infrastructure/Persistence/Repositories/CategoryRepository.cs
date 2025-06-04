using AutoMapper;
using Bookify_Library_mgnt.Data;
using Bookify_Library_mgnt.Dtos.Categories;
using Bookify_Library_mgnt.Helper.Pagination;
using Bookify_Library_mgnt.Models;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bookify_Library_mgnt.Repositpries.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CategoryRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public IQueryable<Category> GetCategoriesAsync()
        {
            var query = _context.Categories.Include(c => c.CategoryBooks)
                .ThenInclude(c => c.Book).AsQueryable();
            return query;
        }
        public async Task<Category> GetByIdAsync(string id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            return category;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            return category;
        }

        public async Task<Category> DeleteCategoryAsync(Category category)
        {
            _context.Categories.Remove(category);
            return category;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
