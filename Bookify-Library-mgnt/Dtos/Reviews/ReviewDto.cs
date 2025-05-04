namespace Bookify_Library_mgnt.Dtos.Reviews
{
    public class ReviewDto
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string BookId { get; set; }
        public string UserId { get; set; }
    }
}
