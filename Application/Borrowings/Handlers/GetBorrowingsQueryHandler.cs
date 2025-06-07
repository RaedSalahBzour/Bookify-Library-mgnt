using Application.Borrowings.Dtos;
using Application.Borrowings.Queries;
using Application.Borrowings.Services;
using Bookify_Library_mgnt.Helper.Pagination;
using MediatR;

namespace Application.Borrowings.Handlers
{
    public class GetBorrowingsQueryHandler : IRequestHandler<GetBorrowingsQuery, PagedResult<BorrowingDto>>
    {
        private readonly IBorrowingService _borrowingService;

        public GetBorrowingsQueryHandler(IBorrowingService borrowingService)
        {
            _borrowingService = borrowingService;
        }
        public async Task<PagedResult<BorrowingDto>> Handle(GetBorrowingsQuery query, CancellationToken cancellationToken)
        {
            var result = await _borrowingService.GetBorrowingsAsync(query.pageNumber, query.pageSize);

            return new PagedResult<BorrowingDto>
            {
                PageNumber = query.pageNumber,
                PageSize = query.pageSize,
                Items = result.Items,
                TotalCount = result.TotalCount
            };
        }
    }
}
