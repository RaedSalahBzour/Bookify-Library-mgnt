using Application.Borrowings.Dtos;
using Bookify_Library_mgnt.Common;
using MediatR;

namespace Application.Borrowings.Commands
{
    public record DeleteBorrowingCommand(string id) : IRequest<Result<BorrowingDto>>;

}
