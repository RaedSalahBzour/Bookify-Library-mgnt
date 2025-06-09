using Application.Categories.Dtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Categories.Commands
{
    public record DeleteCategoryCommand(string Id) : IRequest<CategoryDto>
    {
        [Required(ErrorMessage = "Category ID is required")]
        public string Id { get; init; }
    }
}
