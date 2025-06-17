using Application.Books.Dtos;
using Application.Books.Services;
using AutoMapper;
using Data.Entities;
using Data.Interfaces;
using Service.Exceptions;

namespace Service.Services;

public class BookService(IMapper mapper, IUnitOfWork unitOfWork) : IBookService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    public async Task<List<BookDto>> GetBooksAsync()
    {
        List<Book> books = await _unitOfWork.BookRepository.GetBooksAsync();
        List<BookDto> bookDtos = _mapper.Map<List<BookDto>>(books);
        return bookDtos;
    }

    public async Task<BookDto> GetByIdAsync(string id)
    {
        Book? book = await _unitOfWork.BookRepository.GetBookByIdAsync(id);
        if (book == null)
            throw ExceptionManager
                .ReturnNotFound("Book Not Found", $"Book With Id {id} Was Not Found");
        BookDto bookDto = _mapper.Map<BookDto>(book);
        return bookDto;


    }
    public async Task<BookDto> CreateBookAsync(CreateBookDto CreateBookDto)
    {
        Book book = _mapper.Map<Book>(CreateBookDto);
        book.CategoryBooks = new List<CategoryBook>();
        foreach (string categoryId in CreateBookDto.CategoryIds)
        {
            bool categoryExists = await _unitOfWork.BookRepository.IsCategoryExist(categoryId);
            if (!categoryExists)
                throw ExceptionManager
             .ReturnNotFound("Category Not Found", $"Category With Id {categoryId} Was Not Found");

            book.CategoryBooks.Add(new CategoryBook
            {
                BookId = book.Id,
                CategoryId = categoryId
            });
        }
        await _unitOfWork.BookRepository.AddAsync(book);
        await _unitOfWork.BookRepository.SaveChangesAsync();
        BookDto bookDto = _mapper.Map<BookDto>(book);
        return bookDto;

    }
    public async Task<BookDto> UpdateBookAsync(string id, UpdateBookDto UpdateBookDto)
    {
        Book? book = await _unitOfWork.BookRepository.GetBookByIdAsync(id);
        if (book is null)
            throw ExceptionManager
               .ReturnNotFound("Book Not Found", $"Book With Id {id} Was Not Found");

        _mapper.Map(UpdateBookDto, book);

        List<string> currentCategoryIds = book.CategoryBooks?
            .Select(cb => cb.CategoryId).ToList() ?? new List<string>();

        foreach (string categoryToRemove in currentCategoryIds)
        {
            UpdateBookDto.CategoryIds.Remove(categoryToRemove);
        }

        foreach (string categoryId in UpdateBookDto.CategoryIds)
        {
            bool categoryExists = await _unitOfWork.BookRepository.IsCategoryExist(categoryId);
            if (!categoryExists)
                throw ExceptionManager
        .ReturnNotFound("Category Not Found", $"Category With Id {categoryId} Was Not Found");

            book.CategoryBooks.Add(new CategoryBook
            {
                BookId = book.Id,
                CategoryId = categoryId
            });
        }
        await _unitOfWork.BookRepository.Update(book);
        await _unitOfWork.BookRepository.SaveChangesAsync();
        BookDto bookDto = _mapper.Map<BookDto>(book);
        return bookDto;

    }

    public async Task<BookDto> DeleteBookAsync(string id)
    {
        Book? book = await _unitOfWork.BookRepository.GetByIdAsync(id);
        if (book is null)
            throw ExceptionManager
                .ReturnNotFound("Book Not Found", $"Book With Id {id} Was Not Found");

        await _unitOfWork.BookRepository.Delete(book);
        await _unitOfWork.BookRepository.SaveChangesAsync();
        BookDto bookDto = _mapper.Map<BookDto>(book);
        return bookDto;
    }

}
