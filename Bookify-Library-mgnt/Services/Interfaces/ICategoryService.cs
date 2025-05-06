using Bookify_Library_mgnt.Dtos.Categories;
using Bookify_Library_mgnt.Helper.Pagination;
using Bookify_Library_mgnt.Models;

namespace Bookify_Library_mgnt.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<PagedResult<CategoryDto>> GetCategoriesAsync(int pageNumber = 1, int pageSize = 10);
        Task<CategoryDto> GetByIdAsync(string id);
        Task<Category> CreateCategoryAsync(CreateCategoryDto categoryDto);
        Task<Category> UpdateCategoryAsync(string id, UpdateCategoryDto categoryDto);
        Task<string> DeleteCategoryAsync(string id);
    }
}
