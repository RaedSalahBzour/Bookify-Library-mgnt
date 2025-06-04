using System.ComponentModel.DataAnnotations;

namespace Application.User.Dtos
{
    public class CreateUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
