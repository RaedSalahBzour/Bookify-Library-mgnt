using Application.Users.Dtos;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Users.Commands;

public class UpdateUserCommand : IRequest<UserDto>
{
    [Required(ErrorMessage = "User Name is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "User Name must be between 3 and 50 characters")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; }
    public string? PhoneNumber { get; set; }
    [JsonIgnore]
    public string? id { get; set; }
}
