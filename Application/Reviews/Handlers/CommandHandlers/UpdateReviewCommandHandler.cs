using Application.Reviews.Commands;
using Application.Reviews.Dtos;
using Application.Reviews.Services;
using AutoMapper;
using MediatR;

namespace Application.Reviews.Handlers.CommandHandlers;

public class UpdateReviewCommandHandler(IReviewService reviewService, IMapper mapper)
    : IRequestHandler<UpdateReviewCommand, ReviewDto>
{
    public async Task<ReviewDto> Handle(UpdateReviewCommand command, CancellationToken cancellationToken)
    {
        UpdateReviewDto updateReviewDto = mapper.Map<UpdateReviewDto>(command);
        return await reviewService.UpdateReviewAsync(command.id, updateReviewDto);

    }
}
