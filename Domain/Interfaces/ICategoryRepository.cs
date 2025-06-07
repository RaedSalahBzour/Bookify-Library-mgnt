using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        IQueryable<Category> GetCategories();
        Task<Category> GetCategoryById(string id);

    }
}
