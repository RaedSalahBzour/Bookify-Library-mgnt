using Application.Book.Dtos;
using Application.Book.Services;
using AutoMapper;
using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Helper.Pagination;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Shared;
using FluentValidation;

namespace Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateBookDto> _createValidator;
        private readonly IValidator<UpdateBookDto> _UpdateValidator;
        public BookService(IBookRepository bookRepository, IMapper mapper, IValidator<CreateBookDto> createValidator, IValidator<UpdateBookDto> updateValidator)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _createValidator = createValidator;
            _UpdateValidator = updateValidator;
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

        public async Task<Result<BookDto>> GetByIdAsync(string id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return Result<BookDto>.Fail(ErrorMessages.NotFoundById(id));
            var bookDto = _mapper.Map<BookDto>(book);
            return Result<BookDto>.Ok(bookDto);
        }
        public async Task<Result<Book>> CreateBookAsync(CreateBookDto bookDto)
        {
            var validationResult = await _createValidator.ValidateAsync(bookDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<Book>.Fail(errorMessages);
            }
            var book = _mapper.Map<Book>(bookDto);
            book.CategoryBooks = new List<CategoryBook>();
            foreach (var categoryId in bookDto.CategoryIds)
            {
                var categoryExists = await _bookRepository.IsCategoryExist(categoryId);
                if (!categoryExists)
                {
                    return Result<Book>.Fail(ErrorMessages.NotFoundById(categoryId));
                }
                book.CategoryBooks.Add(new CategoryBook
                {
                    BookId = book.Id,
                    CategoryId = categoryId
                });
            }
            await _bookRepository.CreateBookAsync(book);
            await _bookRepository.SaveChangesAsync();
            return Result<Book>.Ok(book);
        }
        public async Task<Result<Book>> UpdateBookAsync(string id, UpdateBookDto bookDto)
        {
            var validationResult = await _UpdateValidator.ValidateAsync(bookDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<Book>.Fail(errorMessages);
            }

            var book = await _bookRepository.GetByIdAsync(id);
            if (book is null)
            {
                return Result<Book>.Fail(ErrorMessages.NotFoundById(id));
            }

            _mapper.Map(bookDto, book);

            var currentCategoryIds = book.CategoryBooks?
                .Select(cb => cb.CategoryId).ToList() ?? new List<string>();

            foreach (var categoryToRemove in currentCategoryIds)
            {
                bookDto.CategoryIds.Remove(categoryToRemove);
            }

            foreach (var categoryId in bookDto.CategoryIds)
            {
                var categoryExists = await _bookRepository.IsCategoryExist(categoryId);
                if (!categoryExists)
                {
                    return Result<Book>.Fail(ErrorMessages.NotFoundById(categoryId));
                }

                book.CategoryBooks.Add(new CategoryBook
                {
                    BookId = book.Id,
                    CategoryId = categoryId
                });
            }

            await _bookRepository.UpdateBookAsync(book);
            await _bookRepository.SaveChangesAsync();

            return Result<Book>.Ok(book);
        }

        public async Task<Result<Book>> DeleteBookAsync(string id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book is null)
            {
                return Result<Book>.Fail(ErrorMessages.NotFoundById(id));
            }
            await _bookRepository.DeleteBookAsync(book);
            await _bookRepository.SaveChangesAsync();
            return Result<Book>.Ok(book);
        }

    }
}
