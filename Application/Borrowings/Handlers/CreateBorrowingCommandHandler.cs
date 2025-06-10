using Application.Borrowings.Commands;
using Application.Borrowings.Dtos;
using Application.Borrowings.Services;
using AutoMapper;
using MediatR;

namespace Application.Borrowings.Handlers;

public class CreateBorrowingCommandHandler(IBorrowingService borrowingService, IMapper mapper)
    : IRequestHandler<CreateBorrowingCommand, BorrowingDto>
{

    public async Task<BorrowingDto> Handle(CreateBorrowingCommand command, CancellationToken cancellationToken)
    {
        var CreateBorrowingDto = mapper.Map<CreateBorrowingDto>(command);
        return await borrowingService.CreateBorrowingAsync(CreateBorrowingDto);

    }
}

