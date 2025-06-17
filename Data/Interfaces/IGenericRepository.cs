namespace Data.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<List<T>> GetAll(Func<IQueryable<T>, IQueryable<T>> include = null);
    Task<T?> GetByIdAsync(string id);
    Task<T> AddAsync(T entity);
    Task<T> Update(T entity);
    Task<T> Delete(T entity);
    Task SaveChangesAsync();
}
