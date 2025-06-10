using Application.Borrowings.Dtos;
using Application.Borrowings.Queries;
using Application.Borrowings.Services;
using MediatR;

namespace Application.Borrowings.Handlers;

public class GetBorrowingsQueryHandler(IBorrowingService borrowingService) : IRequestHandler<GetBorrowingsQuery, List<BorrowingDto>>
{
    public async Task<List<BorrowingDto>> Handle(GetBorrowingsQuery query, CancellationToken cancellationToken)
    {
        return await borrowingService.GetBorrowingsAsync();
    }
}
