using Application.Reviews.Dtos;
using Bookify_Library_mgnt.Common;
using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Reviews.Commands
{
    public class UpdateReviewCommand : IRequest<Result<ReviewDto>>
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
}
