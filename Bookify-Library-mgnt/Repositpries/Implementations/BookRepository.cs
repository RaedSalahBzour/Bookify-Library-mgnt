using AutoMapper;
using Bookify_Library_mgnt.Data;
using Bookify_Library_mgnt.Dtos.Books;
using Bookify_Library_mgnt.Helper.Pagination;
using Bookify_Library_mgnt.Models;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;

namespace Bookify_Library_mgnt.Repositpries.Implementations
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BookRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResult<BooksDto>> GetBooksAsync(
            int pageNumber = 1, int pageSize = 10,
            string? title = null,
            string? category = null,
            DateOnly? publishtDate = null,
            string? sortBy = null,
            bool descending = false
           )
        {
            var query = _context.Books.AsNoTracking()
                 .Include(b => b.Reviews)
                 .Include(b => b.Borrowings)
                 .Include(b => b.CategoryBooks)
                 .ThenInclude(cb => cb.Category)
                 .AsQueryable();
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

            var totalCount = await query.CountAsync();

            var books = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var booksDto = _mapper.Map<IEnumerable<BooksDto>>(books);

            return new PagedResult<BooksDto>
            {
                Items = booksDto,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<BooksDto> GetByIdAsync(string id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (book == null)
            {
                return null;
            }
            BooksDto bookDto = _mapper.Map<BooksDto>(book);
            return bookDto;
        }
        public async Task<Book> CreateBookAsync(CreateBookDto bookDto)
        {

            var book = _mapper.Map<Book>(bookDto);
            book.CategoryBooks = new List<CategoryBook>();
            foreach (var categoryId in bookDto.CategoryIds)
            {
                var categoryExists = await _context.Categories.AnyAsync(c => c.Id == categoryId);
                if (categoryExists)
                {
                    book.CategoryBooks.Add(new CategoryBook
                    {
                        BookId = book.Id,
                        CategoryId = categoryId
                    });
                }
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> UpdateBookAsync(string id, UpdateBookDto bookDto)
        {
            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (book is null)
            {
                return null;
            }
            _mapper.Map(bookDto, book);
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<string> DeleteBookAsync(string id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (book is null)
            {
                return null;
            }
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return book.Id;

        }
    }
}
