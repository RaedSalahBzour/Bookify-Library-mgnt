using System.Text.Json.Serialization;

namespace Bookify_Library_mgnt.Dtos.Users
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

    }
}
