using Application.Books.Dtos;
using Application.Books.Queries;
using Application.Books.Services;
using MediatR;

namespace Application.Books.Handlers.QueryHandlers;

public class GetBookByIdQueryHandler(IBookService bookService)
    : IRequestHandler<GetBookByIdQuery, BookDto>
{
    public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        return await bookService.GetByIdAsync(request.id);
    }
}
