using Application.Reviews.Commands;
using Application.Reviews.Dtos;
using Application.Reviews.Services;
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

namespace Application.Reviews.Handlers
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, Result<ReviewDto>>
    {
        private readonly IReviewService _reviewService;

        public DeleteReviewCommandHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        public async Task<Result<ReviewDto>> Handle(DeleteReviewCommand command, CancellationToken cancellationToken)
        {
            var result = await _reviewService.DeleteReviewAsync(command.Id);
            if (!result.IsSuccess)
                return Result<ReviewDto>.Fail
                    (ErrorMessages.OperationFailed(nameof(OperationNames.DeleteReview),
                    result.Errors));
            return Result<ReviewDto>.Ok(result.Data);
        }
    }
}
