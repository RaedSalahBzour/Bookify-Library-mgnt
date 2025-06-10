using Application.Categories.Dtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Categories.Commands;

public record CreateCategoryCommand : IRequest<CategoryDto>
{
    [Required(ErrorMessage = "Category Name is required")]
    public string Name { get; init; }

    public string? Description { get; init; }
}
