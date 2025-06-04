namespace Bookify_Library_mgnt.Dtos.Borrowings
{
    public class BorrowingDto
    {
        public DateTime BorrowedOn { get; set; }
        public DateTime ReturnedOn { get; set; }
        public string UserId { get; set; }
        public string BookId { get; set; }
    }
}
