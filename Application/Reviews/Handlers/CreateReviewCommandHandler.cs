using Application.Books.Dtos;
using Application.Books.Services;
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

namespace Application.Reviews.Handlers
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Result<ReviewDto>>
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public CreateReviewCommandHandler(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        public async Task<Result<ReviewDto>> Handle(CreateReviewCommand command, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<CreateReviewDto>(command);
            var result = await _reviewService.CreateReviewAsync(dto);
            if (!result.IsSuccess)
                return Result<ReviewDto>.Fail
                    (ErrorMessages.OperationFailed(nameof(OperationNames.CreateReview),
                    result.Errors));
            return Result<ReviewDto>.Ok(result.Data);
        }
    }
}
