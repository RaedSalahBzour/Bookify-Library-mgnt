using Domain.Entities;

namespace Domain.Interfaces
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
