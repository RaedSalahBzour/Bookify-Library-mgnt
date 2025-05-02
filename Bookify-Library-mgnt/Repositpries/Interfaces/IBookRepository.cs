using Bookify_Library_mgnt.Dtos.Books;
using Bookify_Library_mgnt.Models;

namespace Bookify_Library_mgnt.Repositpries.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<BooksDto>> GetAllAsync();
        Task<BooksDto> GetByIdAsync(string id);
        Task<Book> CreateBookAsync(CreateBookDto bookDto);
        Task<Book> UpdateBookAsync(string id, UpdateBookDto bookDto);
        Task<string> DeleteBookAsync(string id);
    }
}
