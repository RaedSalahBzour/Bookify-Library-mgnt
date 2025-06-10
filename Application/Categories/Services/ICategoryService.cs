using Application.Categories.Dtos;

namespace Application.Categories.Services;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetCategoriesAsync();
    Task<CategoryDto> GetByIdAsync(string id);
    Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto categoryDto);
    Task<CategoryDto> UpdateCategoryAsync(string id, UpdateCategoryDto categoryDto);
    Task<CategoryDto> DeleteCategoryAsync(string id);
}
