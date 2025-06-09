using Application.Books.Dtos;
using Bookify_Library_mgnt.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Books.Commands
{
    public class UpdateBookCommand : IRequest<Result<BookDto>>
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }

        public string Description { get; set; }

        public string CoverUrl { get; set; }

        [Required(ErrorMessage = "ISBN is required")]
        public string Isbn { get; set; }

        [Required(ErrorMessage = "Publish date is required")]
        public DateTime PublishDate { get; set; }
        [MinLength(1, ErrorMessage = "At least one category is required")]
        public List<string> CategoryIds { get; set; } = new List<string>();

        [JsonIgnore]
        public string? Id { get; set; }
    }

}
