using Application.Categories.Dtos;
using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Helper.Pagination;
using Domain.Entities;

namespace Application.Categories.Services
{
    public interface ICategoryService
    {
        Task<PagedResult<CategoryDto>> GetCategoriesAsync(int pageNumber = 1, int pageSize = 10);
        Task<Result<CategoryDto>> GetByIdAsync(string id);
        Task<Result<CategoryDto>> CreateCategoryAsync(CreateCategoryDto categoryDto);
        Task<Result<CategoryDto>> UpdateCategoryAsync(string id, UpdateCategoryDto categoryDto);
        Task<Result<CategoryDto>> DeleteCategoryAsync(string id);
    }
}
