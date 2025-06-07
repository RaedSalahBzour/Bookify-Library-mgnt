using Application.Reviews.Dtos;
using Bookify_Library_mgnt.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reviews.Commands
{
    public class CreateReviewCommand() : IRequest<Result<ReviewDto>>
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string BookId { get; set; }
        public string UserId { get; set; }
    }

}
