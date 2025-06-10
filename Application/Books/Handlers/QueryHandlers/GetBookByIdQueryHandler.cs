using Application.Books.Dtos;
using Application.Books.Queries;
using Application.Books.Services;
using MediatR;

namespace Application.Books.Handlers.QueryHandlers;

public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDto>
{
    private readonly IBookService _bookService;

    public GetBookByIdQueryHandler(IBookService bookService)
    {
        _bookService = bookService;
    }

    public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        return await _bookService.GetByIdAsync(request.id);
    }
}
