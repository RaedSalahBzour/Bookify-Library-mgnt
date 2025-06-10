using Application.Books.Dtos;
using Application.Books.Queries;
using Application.Books.Services;
using MediatR;

namespace Application.Books.Handlers.QueryHandlers;

public class GetBooksQueryHandler(IBookService bookService) : IRequestHandler<GetBooksQuery, List<BookDto>>
{

    public async Task<List<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        return await bookService.GetBooksAsync();

    }
}
