
namespace Data.Entities
{
    public class Review
    {
        public string Id { get; set; }
        public float Rating { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string BookId { get; set; }
        public Book Book { get; set; }
    }
}
