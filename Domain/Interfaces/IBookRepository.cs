using Domain.Entities;

namespace Bookify_Library_mgnt.Repositpries.Interfaces
{
    public interface IBookRepository
    {
        IQueryable<Book> GetBooksAsync();
        Task<Book> GetByIdAsync(string id);
        Task<Book> CreateBookAsync(Book book);
        Task<Book> UpdateBookAsync(Book book);
        Task<Book> DeleteBookAsync(Book book);
        Task SaveChangesAsync();
        Task<bool> IsCategoryExist(string id);
    }
}
