using System.ComponentModel.DataAnnotations;

namespace Application.Books.Dtos
{
    public class CreateBookDto
    {
        [Required(ErrorMessage = "Author is required")]
        [MinLength(3)]
        public string Author { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [MinLength(3)]
        public string Title { get; set; }
        public string? CoverUrl { get; set; }
        [Required(ErrorMessage = "ISBN is required")]
        [MinLength(8)]
        public string ISBN { get; set; }
        [Required(ErrorMessage = "Publish Date is required")]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }
        [MinLength(1, ErrorMessage = "At least one category is required")]
        public List<string> CategoryIds { get; set; }
    }
}
