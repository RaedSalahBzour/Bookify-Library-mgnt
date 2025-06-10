using Application.Categories.Dtos;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Categories.Commands;

public class UpdateCategoryCommand : IRequest<CategoryDto>
{
    [Required(ErrorMessage = "Category Name is required")]
    public string Name { get; init; }
    public string? Description { get; set; }
    [JsonIgnore]
    public string? id { get; set; }
}
