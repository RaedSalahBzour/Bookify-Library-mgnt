using Application.Borrowings.Dtos;
using Application.Borrowings.Queries;
using Application.Borrowings.Services;
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
    public class GetBorrowingByIdQueryHandler : IRequestHandler<GetBorrowingByIdQuery, Result<BorrowingDto>>
    {
        private readonly IBorrowingService _borrowingService;

        public GetBorrowingByIdQueryHandler(IBorrowingService borrowingService)
        {
            _borrowingService = borrowingService;
        }
        public async Task<Result<BorrowingDto>> Handle(GetBorrowingByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _borrowingService.GetBorrowingByIdAsync(query.id);
            if (!result.IsSuccess)
            {
                return Result<BorrowingDto>.Fail(ErrorMessages
                    .OperationFailed(nameof(OperationNames.GetBorrowingById), result.Errors));
            }
            return Result<BorrowingDto>.Ok(result.Data);
        }
    }
}
