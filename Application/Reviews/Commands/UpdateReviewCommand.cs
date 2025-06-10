using Application.Reviews.Dtos;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Reviews.Commands;

public class UpdateReviewCommand : IRequest<ReviewDto>
{
    [Range(1, 10, ErrorMessage = "Rating must be between 1 and 10")]
    public int Rating { get; set; }

    [Required(ErrorMessage = "Comment is required")]
    [StringLength(1000, ErrorMessage = "Comment cannot exceed 1000 characters")]
    public string Comment { get; set; }

    [Required(ErrorMessage = "Book Id is required")]
    public string BookId { get; set; }

    [Required(ErrorMessage = "User Id is required")]
    public string UserId { get; set; }
    [JsonIgnore]
    public string? id { get; set; }
}
