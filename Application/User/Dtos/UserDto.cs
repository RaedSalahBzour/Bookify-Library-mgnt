using Bookify_Library_mgnt.Dtos.Borrowings;
using Bookify_Library_mgnt.Dtos.Reviews;
using System.Text.Json.Serialization;

namespace Bookify_Library_mgnt.Dtos.Users
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
