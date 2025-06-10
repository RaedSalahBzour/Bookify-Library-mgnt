namespace Application.Reviews.Dtos;

public class ReviewDto
{
    public string Id { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public string BookId { get; set; }
    public string UserId { get; set; }
}
