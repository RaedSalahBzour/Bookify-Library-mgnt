using Application.Reviews.Dtos;
using Application.Reviews.Queries;
using Application.Reviews.Services;
using MediatR;

namespace Application.Reviews.Handlers.QeuryHandlers;

public class GetReviewsQueryHandler(IReviewService reviewService)
    : IRequestHandler<GetReviewsQuery, List<ReviewDto>>
{
    public async Task<List<ReviewDto>> Handle(GetReviewsQuery query, CancellationToken cancellationToken)
    {
        return await reviewService.GetReviewsAsync();

    }
}
