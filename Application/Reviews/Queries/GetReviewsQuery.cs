using Application.Reviews.Dtos;
using MediatR;

namespace Application.Reviews.Queries;

public record GetReviewsQuery() : IRequest<List<ReviewDto>>;
