using Application.Books.Dtos;
namespace Application.Books.Services
{
    public interface IBookService
    {
        Task<List<BookDto>> GetBooksAsync();
        Task<BookDto> GetByIdAsync(string id);
        Task<BookDto> CreateBookAsync(CreateBookDto bookDto);
        Task<BookDto> UpdateBookAsync(string id, UpdateBookDto bookDto);
        Task<BookDto> DeleteBookAsync(string id);
    }
}
