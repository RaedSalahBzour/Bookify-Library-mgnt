using AutoMapper;
using Bookify_Library_mgnt.Dtos.Borrowings;
using Bookify_Library_mgnt.Dtos.Categories;
using Bookify_Library_mgnt.Helper.Pagination;
using Bookify_Library_mgnt.Models;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Bookify_Library_mgnt.Services.Interfaces;

namespace Bookify_Library_mgnt.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<CategoryDto>> GetCategoriesAsync(int pageNumber = 1, int pageSize = 10)
        {
            var categories = _categoryRepository.GetCategoriesAsync();
            var paginatedCategories = await categories.ToPaginationForm(pageNumber, pageSize);
            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(paginatedCategories.Items);
            return new PagedResult<CategoryDto>
            {
                TotalCount = paginatedCategories.TotalCount,
                Items = categoriesDto,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        public async Task<CategoryDto> GetByIdAsync(string id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) { return null; }
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }

        public async Task<Category> CreateCategoryAsync(CreateCategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.CreateCategoryAsync(category);
            await _categoryRepository.SaveChangesAsync();
            return category;
        }
        public async Task<Category> UpdateCategoryAsync(string id, UpdateCategoryDto categoryDto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                return null;
            }
            _mapper.Map(categoryDto, category);
            await _categoryRepository.UpdateCategoryAsync(category);
            await _categoryRepository.SaveChangesAsync();
            return category;
        }

        public async Task<string> DeleteCategoryAsync(string id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                return null;
            }
            await _categoryRepository.DeleteCategoryAsync(category);
            await _categoryRepository.SaveChangesAsync();
            return category.Id;
        }

    }
}
