﻿using Application.Borrowings.Dtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Borrowings.Commands;

public class CreateBorrowingCommand() : IRequest<BorrowingDto>
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
