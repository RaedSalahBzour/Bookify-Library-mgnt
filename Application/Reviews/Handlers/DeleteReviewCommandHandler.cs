using Application.Reviews.Commands;
using Application.Reviews.Dtos;
using Application.Reviews.Services;
using MediatR;

namespace Application.Reviews.Handlers;

public class DeleteReviewCommandHandler(IReviewService reviewService)
    : IRequestHandler<DeleteReviewCommand, ReviewDto>
{
    public async Task<ReviewDto> Handle(DeleteReviewCommand command, CancellationToken cancellationToken)
    {
        return await reviewService.DeleteReviewAsync(command.Id);

    }
}
