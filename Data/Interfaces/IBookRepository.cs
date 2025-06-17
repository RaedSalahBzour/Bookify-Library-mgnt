using Data.Entities;

namespace Data.Interfaces;

public interface IBookRepository : IGenericRepository<Book>
{
    Task<List<Book>> GetBooksAsync();
    Task<Book> GetBookByIdAsync(string id);
    Task<bool> IsCategoryExist(string id);
}
