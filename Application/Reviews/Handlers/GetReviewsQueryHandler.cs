using Application.Reviews.Dtos;
using Application.Reviews.Queries;
using Application.Reviews.Services;
using Bookify_Library_mgnt.Helper.Pagination;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reviews.Handlers
{
    public class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, PagedResult<ReviewDto>>
    {
        private readonly IReviewService _reviewService;

        public GetReviewsQueryHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        public async Task<PagedResult<ReviewDto>> Handle(GetReviewsQuery query, CancellationToken cancellationToken)
        {
            var result = await _reviewService.GetReviewsAsync(query.pageNumber, query.pageSize);
            return new PagedResult<ReviewDto>
            {
                PageNumber = query.pageNumber,
                PageSize = query.pageSize,
                Items = result.Items,
                TotalCount = result.TotalCount,

            };
        }
    }
}
