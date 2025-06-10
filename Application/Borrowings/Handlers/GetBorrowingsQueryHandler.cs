using Application.Borrowings.Dtos;
using Application.Borrowings.Queries;
using Application.Borrowings.Services;
using MediatR;

namespace Application.Borrowings.Handlers;

public class GetBorrowingsQueryHandler : IRequestHandler<GetBorrowingsQuery, List<BorrowingDto>>
{
    private readonly IBorrowingService _borrowingService;

    public GetBorrowingsQueryHandler(IBorrowingService borrowingService)
    {
        _borrowingService = borrowingService;
    }
    public async Task<List<BorrowingDto>> Handle(GetBorrowingsQuery query, CancellationToken cancellationToken)
    {
        return await _borrowingService.GetBorrowingsAsync();
    }
}
