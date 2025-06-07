using Application.Borrowings.Dtos;
using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Helper.Pagination;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Borrowings.Queries
{
    public record GetBorrowingsQuery(int pageNumber, int pageSize) : IRequest<PagedResult<BorrowingDto>>;

}
