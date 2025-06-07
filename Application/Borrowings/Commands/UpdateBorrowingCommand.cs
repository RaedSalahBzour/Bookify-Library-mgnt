using Application.Borrowings.Dtos;
using Bookify_Library_mgnt.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Borrowings.Commands
{
    public class UpdateBorrowingCommand() : IRequest<Result<BorrowingDto>>
    {
        public DateTime BorrowedOn { get; set; }
        public DateTime ReturnedOn { get; set; }
        public string UserId { get; set; }
        public string BookId { get; set; }
        [JsonIgnore]
        public string? Id { get; set; }
    }

}
