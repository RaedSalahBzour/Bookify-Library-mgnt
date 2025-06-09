using Application.Borrowings.Dtos;
using MediatR;

namespace Application.Borrowings.Queries
{
    public record GetBorrowingByIdQuery(string id) : IRequest<BorrowingDto>;

}
