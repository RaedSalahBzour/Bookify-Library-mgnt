using Application.Books.Dtos;
using MediatR;

namespace Application.Books.Queries
{
    public record GetBookByIdQuery(string id) : IRequest<BookDto>;

}
