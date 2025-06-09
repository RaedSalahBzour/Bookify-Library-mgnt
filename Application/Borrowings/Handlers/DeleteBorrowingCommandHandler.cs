using Application.Borrowings.Commands;
using Application.Borrowings.Dtos;
using Application.Borrowings.Handlers;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Borrowings.Handlers
{
    public class DeleteBorrowingCommandHandler : IRequestHandler<DeleteBorrowingCommand, Result<BorrowingDto>>
    {
        private readonly IBorrowingService _borrowingService;

        public DeleteBorrowingCommandHandler(IBorrowingService borrowingService)
        {
            _borrowingService = borrowingService;
        }
        public async Task<Result<BorrowingDto>> Handle(DeleteBorrowingCommand command, CancellationToken cancellationToken)
        {
            var result = await _borrowingService.DeleteBorrowingAsync(command.Id);
            if (!result.IsSuccess)
            {
                return Result<BorrowingDto>.Fail(ErrorMessages
                    .OperationFailed(nameof(OperationNames.DeleteBorrowing), result.Errors));
            }
            return Result<BorrowingDto>.Ok(result.Data);
        }
    }
}
