using Application.Books.Dtos;
using MediatR;

namespace Application.Books.Queries
{
    public record GetBooksQuery() : IRequest<List<BookDto>>;

}
