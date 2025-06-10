using Application.Books.Dtos;
using Application.Books.Services;
using AutoMapper;
using Data.Entities;
using Data.Interfaces;

namespace Service.Services;

public class BookService(IMapper mapper, IUnitOfWork unitOfWork) : IBookService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    public async Task<List<BookDto>> GetBooksAsync()
    {
        var query = _unitOfWork.BookRepository.GetBooksAsync();
        return _mapper.Map<List<BookDto>>(query);
    }

    public async Task<BookDto> GetByIdAsync(string id)
    {
        var book = await _unitOfWork.BookRepository.GetBookByIdAsync(id);
        if (book == null) throw new KeyNotFoundException($"Book With Id {id} Was Not Found");
        return _mapper.Map<BookDto>(book);

    }
    public async Task<BookDto> CreateBookAsync(CreateBookDto bookDto)
    {
        var book = _mapper.Map<Book>(bookDto);
        book.CategoryBooks = new List<CategoryBook>();
        foreach (var categoryId in bookDto.CategoryIds)
        {
            var categoryExists = await _unitOfWork.BookRepository.IsCategoryExist(categoryId);
            if (!categoryExists)
            {
                throw new KeyNotFoundException($"Category With Id {categoryId} Was Not Found");
            }
            book.CategoryBooks.Add(new CategoryBook
            {
                BookId = book.Id,
                CategoryId = categoryId
            });
        }
        await _unitOfWork.BookRepository.AddAsync(book);
        await _unitOfWork.BookRepository.SaveChangesAsync();
        return _mapper.Map<BookDto>(book);

    }
    public async Task<BookDto> UpdateBookAsync(string id, UpdateBookDto bookDto)
    {
        var book = await _unitOfWork.BookRepository.GetBookByIdAsync(id);
        if (book is null)
            throw new KeyNotFoundException($"Book With Id {id} Was Not Found");

        _mapper.Map(bookDto, book);

        var currentCategoryIds = book.CategoryBooks?
            .Select(cb => cb.CategoryId).ToList() ?? new List<string>();

        foreach (var categoryToRemove in currentCategoryIds)
        {
            bookDto.CategoryIds.Remove(categoryToRemove);
        }

        foreach (var categoryId in bookDto.CategoryIds)
        {
            var categoryExists = await _unitOfWork.BookRepository.IsCategoryExist(categoryId);
            if (!categoryExists)
            {
                throw new KeyNotFoundException($"Category With Id {categoryId} Was Not Found");
            }

            book.CategoryBooks.Add(new CategoryBook
            {
                BookId = book.Id,
                CategoryId = categoryId
            });
        }
        await _unitOfWork.BookRepository.Update(book);
        await _unitOfWork.BookRepository.SaveChangesAsync();
        return _mapper.Map<BookDto>(book);

    }

    public async Task<BookDto> DeleteBookAsync(string id)
    {
        var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
        if (book is null)
            throw new KeyNotFoundException($"Book With Id {id} Was Not Found");

        await _unitOfWork.BookRepository.Delete(book);
        await _unitOfWork.BookRepository.SaveChangesAsync();
        return _mapper.Map<BookDto>(book);
        ;
    }

}
