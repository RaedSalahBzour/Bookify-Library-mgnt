using Application.Reviews.Commands;
using Application.Reviews.Dtos;
using Application.Reviews.Services;
using AutoMapper;
using MediatR;

namespace Application.Reviews.Handlers
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ReviewDto>
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public CreateReviewCommandHandler(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        public async Task<ReviewDto> Handle(CreateReviewCommand command, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<CreateReviewDto>(command);
            return await _reviewService.CreateReviewAsync(dto);

        }
    }
}
