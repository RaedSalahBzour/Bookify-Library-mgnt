using Application.Reviews.Dtos;
using Bookify_Library_mgnt.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reviews.Queries
{
    public record GetReviewByIdQuery(string id) : IRequest<Result<ReviewDto>>;

}
