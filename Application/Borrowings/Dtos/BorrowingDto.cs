namespace Application.Borrowings.Dtos;

public class BorrowingDto
{
    public string Id { get; set; }
    public DateTime BorrowedOn { get; set; }
    public DateTime ReturnedOn { get; set; }
    public string UserId { get; set; }
    public string BookId { get; set; }
}
