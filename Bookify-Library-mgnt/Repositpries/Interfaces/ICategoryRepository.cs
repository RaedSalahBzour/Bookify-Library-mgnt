using Bookify_Library_mgnt.Dtos.Categories;
using Bookify_Library_mgnt.Models;

namespace Bookify_Library_mgnt.Repositpries.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(string id);
        Task<Category> CreateCategoryAsync(CreateCategoryDto categoryDto);
        Task<Category> UpdateCategoryAsync(string id, UpdateCategoryDto categoryDto);
        Task<string> DeleteCategoryAsync(string id);
    }
}
