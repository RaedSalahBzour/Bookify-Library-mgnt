using Application.Borrowings.Dtos;
using Application.Reviews.Dtos;

namespace Application.Users.Dtos
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<ReviewDto> Reviews { get; set; }
        public List<BorrowingDto> Borrowings { get; set; }

    }
}
