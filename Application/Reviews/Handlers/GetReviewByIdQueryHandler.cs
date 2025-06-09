using Application.Reviews.Dtos;
using Application.Reviews.Queries;
using Application.Reviews.Services;
using MediatR;

namespace Application.Reviews.Handlers
{
    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, ReviewDto>
    {
        private readonly IReviewService _reviewService;

        public GetReviewByIdQueryHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        public async Task<ReviewDto> Handle(GetReviewByIdQuery query, CancellationToken cancellationToken)
        {
            return await _reviewService.GetReviewByIdAsync(query.id);

        }
    }
}
