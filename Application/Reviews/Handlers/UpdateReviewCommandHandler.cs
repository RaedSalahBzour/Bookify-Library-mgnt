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
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, Result<ReviewDto>>
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public UpdateReviewCommandHandler(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }
        public async Task<Result<ReviewDto>> Handle(UpdateReviewCommand command, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<UpdateReviewDto>(command);
            var result = await _reviewService.UpdateReviewAsync(command.id, dto);
            if (!result.IsSuccess)
                return Result<ReviewDto>.Fail
                    (ErrorMessages.OperationFailed(nameof(OperationNames.UpdateBook),
                    result.Errors));
            return Result<ReviewDto>.Ok(result.Data);
        }
    }
}
