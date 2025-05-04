using AutoMapper;
using Bookify_Library_mgnt.Data;
using Bookify_Library_mgnt.Dtos.Categories;
using Bookify_Library_mgnt.Helper;
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


        public async Task<PagedResult<CategoryDto>> GetCategoriesAsync(int pageNumber = 1, int pageSize = 10)
        {
            var query = _context.Categories.Include(c => c.CategoryBooks)
                .ThenInclude(c => c.Book).AsQueryable();
            var totalCount = await query.CountAsync();
            var categories = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return new PagedResult<CategoryDto>
            {
                TotalCount = totalCount,
                Items = categoriesDto,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        public async Task<CategoryDto> GetByIdAsync(string id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return null;
            }
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }
        public async Task<Category> CreateCategoryAsync(CreateCategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                return null;
            }
            var category = _mapper.Map<Category>(categoryDto);
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;

        }

        public async Task<Category> UpdateCategoryAsync(string id, UpdateCategoryDto categoryDto)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category is null)
            {
                return null;
            }
            _mapper.Map(categoryDto, category);
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<string> DeleteCategoryAsync(string id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category is null)
            {
                return null;
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return category.Id;
        }

    }
}
