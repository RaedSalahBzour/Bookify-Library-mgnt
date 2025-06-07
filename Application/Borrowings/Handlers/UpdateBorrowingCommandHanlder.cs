using Application.Books.Dtos;
using Application.Books.Services;
using Application.Borrowings.Commands;
using Application.Borrowings.Dtos;
using Application.Borrowings.Services;
using AutoMapper;
using Bookify_Library_mgnt.Common;
using Domain.Enums;
using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Borrowings.Handlers
{
    internal class UpdateBorrowingCommandHanlder : IRequestHandler<UpdateBorrowingCommand, Result<BorrowingDto>>
    {
        private readonly IBorrowingService _borrowingService;
        private readonly IMapper _mapper;

        public UpdateBorrowingCommandHanlder(IBorrowingService borrowingService, IMapper mapper)
        {
            _borrowingService = borrowingService;
            _mapper = mapper;
        }
        public async Task<Result<BorrowingDto>> Handle(UpdateBorrowingCommand command, CancellationToken cancellationToken)
        {
            var updateBorrowingDto = _mapper.Map<UpdateBorrowingDto>(command);
            var result = await _borrowingService.UpdateBorrowingAsync(command.Id, updateBorrowingDto);
            if (!result.IsSuccess)
            {
                return Result<BorrowingDto>.Fail(ErrorMessages.OperationFailed(nameof(OperationNames.UpdateBorrowing), result.Errors));
            }
            return Result<BorrowingDto>.Ok(result.Data);
        }
    }
}
