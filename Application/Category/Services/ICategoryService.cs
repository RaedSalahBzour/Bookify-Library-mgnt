using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Dtos.Categories;
using Bookify_Library_mgnt.Helper.Pagination;
using Domain.Entities;

namespace Bookify_Library_mgnt.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<PagedResult<CategoryDto>> GetCategoriesAsync(int pageNumber = 1, int pageSize = 10);
        Task<Result<CategoryDto>> GetByIdAsync(string id);
        Task<Result<Category>> CreateCategoryAsync(CreateCategoryDto categoryDto);
        Task<Result<Category>> UpdateCategoryAsync(string id, UpdateCategoryDto categoryDto);
        Task<Result<Category>> DeleteCategoryAsync(string id);
    }
}
