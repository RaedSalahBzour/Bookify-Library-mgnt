using Application.Books.Dtos;
using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Helper.Pagination;
using MediatR;

namespace Application.Books.Queries
{
    public record GetBooksQuery(
        int pageNumber = 1,
        int pageSize = 10,
        string? title = null,
        string? category = null,
        DateOnly? publishtDate = null,
        string? sortBy = null,
        bool descending = false) : IRequest<PagedResult<BookDto>>;

}
