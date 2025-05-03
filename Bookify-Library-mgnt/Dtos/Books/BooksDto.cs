using Bookify_Library_mgnt.Dtos.Reviews;
using Bookify_Library_mgnt.Models;

namespace Bookify_Library_mgnt.Dtos.Books
{
    public class BooksDto
    {
        public string Id { get; set; }
        public string Author { get; set; }
        public string? Description { get; set; }
        public string Title { get; set; }
        public string? CoverUrl { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishDate { get; set; }
        public List<ReviewDto> Reviews { get; set; }
        public List<Borrowing> Borrowings { get; set; }
    }
}
