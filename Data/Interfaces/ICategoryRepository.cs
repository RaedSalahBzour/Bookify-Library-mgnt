using Data.Entities;

namespace Data.Interfaces;

public interface ICategoryRepository : IGenericRepository<Category>
{
    List<Category> GetCategories();
    Task<Category> GetCategoryById(string id);

}
