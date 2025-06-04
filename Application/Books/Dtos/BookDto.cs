using Application.Borrowings.Dtos;
using Application.Reviews.Dtos;

namespace Application.Books.Dtos
{
    public class BookDto
    {
        public string Id { get; set; }
        public string Author { get; set; }
        public string? Description { get; set; }
        public string Title { get; set; }
        public string? CoverUrl { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishDate { get; set; }
        public List<ReviewDto> Reviews { get; set; }
        public List<BorrowingDto> Borrowings { get; set; }
    }
}
