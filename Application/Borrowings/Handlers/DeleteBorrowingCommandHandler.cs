using Application.Borrowings.Commands;
using Application.Borrowings.Dtos;
using Application.Borrowings.Services;
using MediatR;

namespace Application.Borrowings.Handlers
{
    public class DeleteBorrowingCommandHandler : IRequestHandler<DeleteBorrowingCommand, BorrowingDto>
    {
        private readonly IBorrowingService _borrowingService;

        public DeleteBorrowingCommandHandler(IBorrowingService borrowingService)
        {
            _borrowingService = borrowingService;
        }
        public async Task<BorrowingDto> Handle(DeleteBorrowingCommand command, CancellationToken cancellationToken)
        {
            return await _borrowingService.DeleteBorrowingAsync(command.Id);

        }
    }
}
