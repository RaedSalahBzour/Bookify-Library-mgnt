using Application.Books.Dtos;
using Application.Books.Queries;
using Application.Books.Services;
using Bookify_Library_mgnt.Helper.Pagination;
using MediatR;

namespace Application.Books.Handlers.QueryHandlers
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, PagedResult<BookDto>>
    {
        private readonly IBookService _bookService;

        public GetBooksQueryHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<PagedResult<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var result = await _bookService.GetBooksAsync(
                request.pageNumber, request.pageSize,
                request.title, request.category,
                request.publishtDate, request.sortBy, request.descending);
            return new PagedResult<BookDto>
            {
                Items = result.Items,
                PageNumber = request.pageNumber,
                PageSize = request.pageSize,
                TotalCount = result.TotalCount,
            };
        }
    }
}
