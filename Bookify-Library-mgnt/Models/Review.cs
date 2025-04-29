namespace Bookify_Library_mgnt.Models
{
    public class Review
    {
        public string Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string BookId { get; set; }
        public Book Book { get; set; }
    }
}
