using Application.Borrowings.Dtos;
using MediatR;

namespace Application.Borrowings.Queries
{
    public record GetBorrowingsQuery() : IRequest<List<BorrowingDto>>;

}
