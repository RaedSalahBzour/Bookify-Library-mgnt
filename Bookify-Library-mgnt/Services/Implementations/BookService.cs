using AutoMapper;
using Bookify_Library_mgnt.Dtos.Books;
using Bookify_Library_mgnt.Helper.Pagination;
using Bookify_Library_mgnt.Models;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Bookify_Library_mgnt.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Bookify_Library_mgnt.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }


        public async Task<PagedResult<BookDto>> GetBooksAsync(int pageNumber = 1,
            int pageSize = 10, string? title = null,
            string? category = null, DateOnly? publishtDate = null,
            string? sortBy = null, bool descending = false)
        {
            var query = _bookRepository.GetBooksAsync();
            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(b => b.Title.Contains(title));
            }

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(b => b.CategoryBooks
                                          .Any(cb => cb.Category.Name == category));
            }

            if (publishtDate.HasValue)
            {
                query = query.Where(b => DateOnly.FromDateTime(b.PublishDate) == publishtDate.Value);
            }
            query = sortBy?.ToLower() switch
            {
                "rating" => descending
                    ? query.OrderByDescending(b => b.Reviews.Any() ? b.Reviews.Average(r => r.Rating) : 0)
                    : query.OrderBy(b => b.Reviews.Any() ? b.Reviews.Average(r => r.Rating) : 0),
                "title" => descending
                    ? query.OrderByDescending(b => b.Title)
                    : query.OrderBy(b => b.Title),
                "publishdate" => descending
                ? query.OrderByDescending(b => b.PublishDate)
                    : query.OrderBy(b => b.PublishDate),

                _ => query.OrderBy(b => b.Title)
            };
            var books = await query.ToPaginationForm(pageNumber, pageSize);
            var booksDto = _mapper.Map<IEnumerable<BookDto>>(books.Items);
            return new PagedResult<BookDto>
            {
                TotalCount = books.TotalCount,
                Items = booksDto,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };
        }

        public async Task<BookDto> GetByIdAsync(string id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return null;
            var bookDto = _mapper.Map<BookDto>(book);
            return bookDto;
        }
        public async Task<Book> CreateBookAsync(CreateBookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            book.CategoryBooks = new List<CategoryBook>();
            foreach (var categoryId in bookDto.CategoryIds)
            {
                var categoryExists = await _bookRepository.IsCategoryExist(categoryId);
                if (categoryExists)
                {
                    book.CategoryBooks.Add(new CategoryBook
                    {
                        BookId = book.Id,
                        CategoryId = categoryId
                    });
                }
            }
            await _bookRepository.CreateBookAsync(book);
            await _bookRepository.SaveChangesAsync();
            return book;
        }
        public async Task<Book> UpdateBookAsync(string id, UpdateBookDto bookDto)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book is null)
            {
                return null;
            }
            _mapper.Map(bookDto, book);
            await _bookRepository.UpdateBookAsync(book);
            await _bookRepository.SaveChangesAsync();
            return book;

        }

        public async Task<string> DeleteBookAsync(string id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book is null)
            {
                return null;
            }
            await _bookRepository.DeleteBookAsync(book);
            await _bookRepository.SaveChangesAsync();
            return book.Id;
        }

    }
}
