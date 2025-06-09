using Application.Books.Dtos;
using Application.Books.Queries;
using Application.Books.Services;
using Bookify_Library_mgnt.Helper.Pagination;
using MediatR;

namespace Application.Books.Handlers.QueryHandlers
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<BookDto>>
    {
        private readonly IBookService _bookService;

        public GetBooksQueryHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<List<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            return await _bookService.GetBooksAsync();

        }
    }
}
