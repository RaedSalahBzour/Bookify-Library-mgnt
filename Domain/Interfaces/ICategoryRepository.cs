using Data.Entities;

namespace Data.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        IQueryable<Category> GetCategories();
        Task<Category> GetCategoryById(string id);

    }
}
