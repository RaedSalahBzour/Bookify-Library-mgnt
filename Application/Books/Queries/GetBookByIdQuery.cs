using Application.Books.Dtos;
using Bookify_Library_mgnt.Common;
using MediatR;

namespace Application.Books.Queries
{
    public record GetBookByIdQuery(string id) : IRequest<Result<BookDto>>;

}
