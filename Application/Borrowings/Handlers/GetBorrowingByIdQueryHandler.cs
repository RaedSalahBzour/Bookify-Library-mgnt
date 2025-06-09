using Application.Borrowings.Dtos;
using Application.Borrowings.Queries;
using Application.Borrowings.Services;
using MediatR;

namespace Application.Borrowings.Handlers
{
    public class GetBorrowingByIdQueryHandler : IRequestHandler<GetBorrowingByIdQuery, BorrowingDto>
    {
        private readonly IBorrowingService _borrowingService;

        public GetBorrowingByIdQueryHandler(IBorrowingService borrowingService)
        {
            _borrowingService = borrowingService;
        }
        public async Task<BorrowingDto> Handle(GetBorrowingByIdQuery query, CancellationToken cancellationToken)
        {
            return await _borrowingService.GetBorrowingByIdAsync(query.id);

        }
    }
}
