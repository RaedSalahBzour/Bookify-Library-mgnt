using Application.Borrowings.Commands;
using Application.Borrowings.Dtos;
using Application.Borrowings.Services;
using AutoMapper;
using MediatR;

namespace Application.Borrowings.Handlers.CommandHandlers;

internal class UpdateBorrowingCommandHanlder(IBorrowingService borrowingService, IMapper mapper)
    : IRequestHandler<UpdateBorrowingCommand, BorrowingDto>
{
    public async Task<BorrowingDto> Handle(UpdateBorrowingCommand command, CancellationToken cancellationToken)
    {
        UpdateBorrowingDto updateBorrowingDto = mapper.Map<UpdateBorrowingDto>(command);
        return await borrowingService.UpdateBorrowingAsync(command.Id, updateBorrowingDto);

    }
}
