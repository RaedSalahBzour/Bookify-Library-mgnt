using Bookify_Library_mgnt.Common;
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
        Task<Result<BookDto>> GetByIdAsync(string id);
        Task<Result<Book>> CreateBookAsync(CreateBookDto bookDto);
        Task<Result<Book>> UpdateBookAsync(string id, UpdateBookDto bookDto);
        Task<Result<Book>> DeleteBookAsync(string id);
    }
}
