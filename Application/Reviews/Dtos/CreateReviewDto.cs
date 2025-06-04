namespace Application.Reviews.Dtos
{
    public class CreateReviewDto
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string BookId { get; set; }
        public string UserId { get; set; }

    }
}
