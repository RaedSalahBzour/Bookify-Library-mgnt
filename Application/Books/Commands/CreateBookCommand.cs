using Application.Books.Dtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Books.Commands
{
    public record CreateBookCommand : IRequest<BookDto>
    {
        [Required(ErrorMessage = "Author is required")]
        public string Author { get; init; }

        public string? Description { get; init; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; init; }

        public string? CoverUrl { get; init; }

        [Required(ErrorMessage = "ISBN is required")]
        public string ISBN { get; init; }

        [Required(ErrorMessage = "Publish Date is required")]
        public DateTime PublishDate { get; init; }
        [Required(ErrorMessage = "Category Id is required")]
        [MinLength(1, ErrorMessage = "At least one category is required")]
        public List<string> CategoryIds { get; init; } = new();
    }

}
