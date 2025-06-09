using Application.Borrowings.Dtos;
using Bookify_Library_mgnt.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Borrowings.Commands
{
    public class CreateBorrowingCommand() : IRequest<Result<BorrowingDto>>
    {
        [Required(ErrorMessage = "BorrowedOn date is required")]
        [DataType(DataType.Date)]
        public DateTime BorrowedOn { get; set; }
        [Required(ErrorMessage = "Returned On date is required")]
        [DataType(DataType.Date)]
        public DateTime ReturnedOn { get; set; }

        [Required(ErrorMessage = "UserId is required")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "BookId is required")]
        public string BookId { get; set; }
    }

}
