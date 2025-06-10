using System.ComponentModel.DataAnnotations;

namespace Application.Reviews.Dtos;

public class UpdateReviewDto
{
    [Range(1, 10, ErrorMessage = "Rating must be between 1 and 10")]
    public int Rating { get; set; }

    [Required(ErrorMessage = "Comment is required")]
    [StringLength(1000, ErrorMessage = "Comment cannot exceed 1000 characters")]
    public string Comment { get; set; }

    [Required(ErrorMessage = "BookId is required")]
    public string BookId { get; set; }

    [Required(ErrorMessage = "UserId is required")]
    public string UserId { get; set; }
}
