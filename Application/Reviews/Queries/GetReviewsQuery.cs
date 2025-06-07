using Application.Reviews.Dtos;
using Bookify_Library_mgnt.Helper.Pagination;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reviews.Queries
{
    public record GetReviewsQuery(int pageNumber, int pageSize) : IRequest<PagedResult<ReviewDto>>;

}
