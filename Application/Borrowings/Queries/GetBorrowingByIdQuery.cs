using Application.Borrowings.Dtos;
using Bookify_Library_mgnt.Common;
using MediatR;

namespace Application.Borrowings.Queries
{
    public record GetBorrowingByIdQuery(string id) : IRequest<Result<BorrowingDto>>;

}
