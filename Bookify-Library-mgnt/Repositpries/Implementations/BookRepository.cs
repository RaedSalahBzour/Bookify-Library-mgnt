using AutoMapper;
using Bookify_Library_mgnt.Data;
using Bookify_Library_mgnt.Dtos.Books;
using Bookify_Library_mgnt.Helper;
using Bookify_Library_mgnt.Models;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<PagedResult<BooksDto>> GetBooksAsync(int pageNumber = 1, int pageSize = 10)
        {
            var query = _context.Books
                .Include(b => b.Reviews)
                .Include(b => b.Borrowings)
                .AsQueryable();

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
