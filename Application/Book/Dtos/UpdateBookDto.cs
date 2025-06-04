namespace Application.Book.Dtos
{
    public class UpdateBookDto
    {
        public string Author { get; set; }
        public string? Description { get; set; }
        public string Title { get; set; }
        public string? CoverUrl { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishDate { get; set; }
        public List<string> CategoryIds { get; set; }
    }
}
