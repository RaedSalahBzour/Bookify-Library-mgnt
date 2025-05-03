using System.ComponentModel.DataAnnotations;

namespace Bookify_Library_mgnt.Dtos.Users
{
    public class UpdateUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
