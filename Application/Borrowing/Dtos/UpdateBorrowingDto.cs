namespace Application.Borrowing.Dtos
{
    public class UpdateBorrowingDto
    {
        public DateTime BorrowedOn { get; set; }
        public DateTime ReturnedOn { get; set; }
        public string UserId { get; set; }
        public string BookId { get; set; }

    }
}
