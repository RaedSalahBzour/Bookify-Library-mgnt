using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        IQueryable<Book> GetBooksAsync();
        Task<Book> GetBookByIdAsync(string id);
        Task<bool> IsCategoryExist(string id);
    }
}
