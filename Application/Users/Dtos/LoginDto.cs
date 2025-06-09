using System.ComponentModel.DataAnnotations;

namespace Application.Users.Dtos
{
    public class LoginDto
    {

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }
    }

}


