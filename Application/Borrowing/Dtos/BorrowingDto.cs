namespace Application.Borrowing.Dtos
{
    public class BorrowingDto
    {
        public DateTime BorrowedOn { get; set; }
        public DateTime ReturnedOn { get; set; }
        public string UserId { get; set; }
        public string BookId { get; set; }
    }
}
