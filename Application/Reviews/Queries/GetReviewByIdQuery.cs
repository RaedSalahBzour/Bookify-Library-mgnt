using Application.Reviews.Dtos;
using MediatR;

namespace Application.Reviews.Queries
{
    public record GetReviewByIdQuery(string id) : IRequest<ReviewDto>;

}
