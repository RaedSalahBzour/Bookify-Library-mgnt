using Data.Entities;

namespace Data.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        IQueryable<Book> GetBooksAsync();
        Task<Book> GetBookByIdAsync(string id);
        Task<bool> IsCategoryExist(string id);
    }
}
