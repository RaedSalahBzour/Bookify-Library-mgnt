using Application.Borrowings.Dtos;
using Application.Borrowings.Queries;
using Application.Borrowings.Services;
using MediatR;

namespace Application.Borrowings.Handlers;

public class GetBorrowingByIdQueryHandler(IBorrowingService borrowingService) : IRequestHandler<GetBorrowingByIdQuery, BorrowingDto>
{
    public async Task<BorrowingDto> Handle(GetBorrowingByIdQuery query, CancellationToken cancellationToken)
    {
        return await borrowingService.GetBorrowingByIdAsync(query.id);

    }
}
