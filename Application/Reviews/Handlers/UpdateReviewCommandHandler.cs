using Application.Reviews.Commands;
using Application.Reviews.Dtos;
using Application.Reviews.Services;
using AutoMapper;
using MediatR;

namespace Application.Reviews.Handlers
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, ReviewDto>
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public UpdateReviewCommandHandler(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }
        public async Task<ReviewDto> Handle(UpdateReviewCommand command, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<UpdateReviewDto>(command);
            return await _reviewService.UpdateReviewAsync(command.id, dto);

        }
    }
}
