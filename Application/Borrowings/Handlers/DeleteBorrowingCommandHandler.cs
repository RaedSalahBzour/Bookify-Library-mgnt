using Application.Borrowings.Commands;
using Application.Borrowings.Dtos;
using Application.Borrowings.Services;
using MediatR;

namespace Application.Borrowings.Handlers;

public class DeleteBorrowingCommandHandler(IBorrowingService borrowingService) : IRequestHandler<DeleteBorrowingCommand, BorrowingDto>
{
    public async Task<BorrowingDto> Handle(DeleteBorrowingCommand command, CancellationToken cancellationToken)
    {
        return await borrowingService.DeleteBorrowingAsync(command.Id);

    }
}
