using System.ComponentModel.DataAnnotations;

namespace Application.Categories.Dtos
{
    public class UpdateCategoryDto
    {
        [Required(ErrorMessage = "Category Name is required")]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
