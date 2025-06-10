using Application.Reviews.Dtos;
using Application.Reviews.Queries;
using Application.Reviews.Services;
using MediatR;

namespace Application.Reviews.Handlers;

public class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, List<ReviewDto>>
{
    private readonly IReviewService _reviewService;

    public GetReviewsQueryHandler(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }
    public async Task<List<ReviewDto>> Handle(GetReviewsQuery query, CancellationToken cancellationToken)
    {
        return await _reviewService.GetReviewsAsync();

    }
}
