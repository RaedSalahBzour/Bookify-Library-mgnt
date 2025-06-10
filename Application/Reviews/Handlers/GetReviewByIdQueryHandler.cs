using Application.Reviews.Dtos;
using Application.Reviews.Queries;
using Application.Reviews.Services;
using MediatR;

namespace Application.Reviews.Handlers
{
    public class GetReviewByIdQueryHandler(IReviewService reviewService)
        : IRequestHandler<GetReviewByIdQuery, ReviewDto>
    {
        public async Task<ReviewDto> Handle(GetReviewByIdQuery query, CancellationToken cancellationToken)
        {
            return await reviewService.GetReviewByIdAsync(query.id);

        }
    }
}
