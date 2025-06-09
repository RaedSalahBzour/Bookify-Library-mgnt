using Application.Reviews.Commands;
using Application.Reviews.Dtos;
using Application.Reviews.Services;
using MediatR;

namespace Application.Reviews.Handlers
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, ReviewDto>
    {
        private readonly IReviewService _reviewService;

        public DeleteReviewCommandHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        public async Task<ReviewDto> Handle(DeleteReviewCommand command, CancellationToken cancellationToken)
        {
            return await _reviewService.DeleteReviewAsync(command.Id);

        }
    }
}
