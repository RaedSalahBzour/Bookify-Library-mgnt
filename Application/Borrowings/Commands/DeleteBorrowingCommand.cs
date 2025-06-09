using Application.Borrowings.Dtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Borrowings.Commands
{
    public record DeleteBorrowingCommand(string Id) : IRequest<BorrowingDto>
    {
        [Required(ErrorMessage = "Borrowing ID is required")]
        public string Id { get; init; }
    }
}
