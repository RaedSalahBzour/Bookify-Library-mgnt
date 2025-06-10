using System.ComponentModel.DataAnnotations;

namespace Application.Categories.Dtos;

public class CreateCategoryDto
{
    [Required(ErrorMessage = "Category Name is required")]
    public string Name { get; init; }
    public string? Description { get; set; }
}
