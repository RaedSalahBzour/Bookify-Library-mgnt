using Application.Books.Dtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Books.Commands
{
    public record DeleteBookCommand(string Id) : IRequest<BookDto>
    {
        [Required(ErrorMessage = "Book Id is required")]
        public string Id { get; init; }
    }
}
