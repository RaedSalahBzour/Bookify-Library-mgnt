using Application.Reviews.Commands;
using Application.Reviews.Dtos;
using Application.Reviews.Services;
using AutoMapper;
using MediatR;

namespace Application.Reviews.Handlers.CommandHandlers;

public class CreateReviewCommandHandler(IReviewService reviewService, IMapper mapper)
    : IRequestHandler<CreateReviewCommand, ReviewDto>
{

    public async Task<ReviewDto> Handle(CreateReviewCommand command, CancellationToken cancellationToken)
    {
        CreateReviewDto createReviewDto = mapper.Map<CreateReviewDto>(command);
        return await reviewService.CreateReviewAsync(createReviewDto);

    }
}
