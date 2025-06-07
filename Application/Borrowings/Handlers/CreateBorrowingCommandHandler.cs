using Application.Borrowings.Commands;
using Application.Borrowings.Dtos;
using Application.Borrowings.Services;
using Application.Categories.Dtos;
using AutoMapper;
using Bookify_Library_mgnt.Common;
using Domain.Enums;
using Domain.Shared;
using MediatR;

namespace Application.Borrowings.Handlers
{
    public class CreateBorrowingCommandHandler : IRequestHandler<CreateBorrowingCommand, Result<BorrowingDto>>
    {
        private readonly IBorrowingService _borrowingService;
        private readonly IMapper _mapper;

        public CreateBorrowingCommandHandler(IBorrowingService borrowingService, IMapper mapper)
        {
            _borrowingService = borrowingService;
            _mapper = mapper;
        }
        public async Task<Result<BorrowingDto>> Handle(CreateBorrowingCommand command, CancellationToken cancellationToken)
        {
            var CreateBorrowingDto = _mapper.Map<CreateBorrowingDto>(command);
            var result = await _borrowingService.CreateBorrowingAsync(CreateBorrowingDto);
            if (!result.IsSuccess)
            {
                return Result<BorrowingDto>.Fail(ErrorMessages.OperationFailed(nameof(OperationNames.CreateBorrowing), result.Errors));
            }
            return Result<BorrowingDto>.Ok(result.Data);
        }
    }
}

