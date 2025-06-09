using Application.Borrowings.Dtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Borrowings.Commands
{
    public class DeleteBorrowingCommand : IRequest<BorrowingDto>
    {
        [Required(ErrorMessage = "Borrowing ID is required")]
        public string Id { get; init; }
    }
}
