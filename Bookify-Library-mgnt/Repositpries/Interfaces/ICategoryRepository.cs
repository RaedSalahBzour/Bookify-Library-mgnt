using Bookify_Library_mgnt.Dtos.Categories;
using Bookify_Library_mgnt.Helper.Pagination;
using Bookify_Library_mgnt.Models;

namespace Bookify_Library_mgnt.Repositpries.Interfaces
{
    public interface ICategoryRepository
    {
        IQueryable<Category> GetCategoriesAsync();
        Task<Category> GetByIdAsync(string id);
        Task<Category> CreateCategoryAsync(Category category);
        Task<Category> UpdateCategoryAsync(Category category);
        Task<Category> DeleteCategoryAsync(Category category);
        Task SaveChangesAsync();
    }
}
