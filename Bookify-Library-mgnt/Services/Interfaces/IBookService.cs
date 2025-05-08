using Bookify_Library_mgnt.Dtos.Books;
using Bookify_Library_mgnt.Helper.Pagination;
using Bookify_Library_mgnt.Models;

namespace Bookify_Library_mgnt.Services.Interfaces
{
    public interface IBookService
    {
        Task<PagedResult<BookDto>> GetBooksAsync(int pageNumber = 1, int pageSize = 10, string? title = null,
            string? category = null,
            DateOnly? publishtDate = null,
            string? sortBy = null,
            bool descending = false);
        Task<BookDto> GetByIdAsync(string id);
        Task<Book> CreateBookAsync(CreateBookDto bookDto);
        Task<Book> UpdateBookAsync(string id, UpdateBookDto bookDto);
        Task<string> DeleteBookAsync(string id);
    }
}
