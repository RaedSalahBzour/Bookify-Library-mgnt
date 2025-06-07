using Application.Reviews.Dtos;
using Application.Reviews.Queries;
using Application.Reviews.Services;
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
    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, Result<ReviewDto>>
    {
        private readonly IReviewService _reviewService;

        public GetReviewByIdQueryHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        public async Task<Result<ReviewDto>> Handle(GetReviewByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _reviewService.GetReviewByIdAsync(query.id);
            if (!result.IsSuccess)
                return Result<ReviewDto>.Fail
                    (ErrorMessages.OperationFailed(nameof(OperationNames.GetReviewById),
                    result.Errors));
            return Result<ReviewDto>.Ok(result.Data);
        }
    }
}
