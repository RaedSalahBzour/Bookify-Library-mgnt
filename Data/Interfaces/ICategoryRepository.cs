using Data.Entities;

namespace Data.Interfaces;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<bool> ExistsByNameAsync(string name);
    Task<List<Category>> GetCategories();
    Task<Category> GetCategoryById(string id);

}
