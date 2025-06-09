using Application.Borrowings.Dtos;
using Bookify_Library_mgnt.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Borrowings.Commands
{
    public record DeleteBorrowingCommand(string Id) : IRequest<Result<BorrowingDto>>
    {
        [Required(ErrorMessage = "Borrowing ID is required")]
        public string Id { get; init; }
    }
}
