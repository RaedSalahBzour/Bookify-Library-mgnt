using Application.Borrowings.Commands;
using Application.Borrowings.Dtos;
using Application.Borrowings.Services;
using AutoMapper;
using MediatR;

namespace Application.Borrowings.Handlers;

public class CreateBorrowingCommandHandler : IRequestHandler<CreateBorrowingCommand, BorrowingDto>
{
    private readonly IBorrowingService _borrowingService;
    private readonly IMapper _mapper;

    public CreateBorrowingCommandHandler(IBorrowingService borrowingService, IMapper mapper)
    {
        _borrowingService = borrowingService;
        _mapper = mapper;
    }
    public async Task<BorrowingDto> Handle(CreateBorrowingCommand command, CancellationToken cancellationToken)
    {
        var CreateBorrowingDto = _mapper.Map<CreateBorrowingDto>(command);
        return await _borrowingService.CreateBorrowingAsync(CreateBorrowingDto);

    }
}

