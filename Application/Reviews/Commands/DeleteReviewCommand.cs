using Application.Reviews.Dtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Reviews.Commands
{
    public class DeleteReviewCommand : IRequest<ReviewDto>
    {
        [Required(ErrorMessage = "Review ID is required")]
        public string Id { get; init; }
    }
}
