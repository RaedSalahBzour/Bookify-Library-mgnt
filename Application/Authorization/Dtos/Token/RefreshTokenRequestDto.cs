using System.ComponentModel.DataAnnotations;

namespace Application.Authorization.Dtos.Token;

public class RefreshTokenRequestDto
{
    [Required(ErrorMessage = "User Id is required")]
    public string UserId { get; set; }
    [Required(ErrorMessage = "Refresh Token is required")]
    public required string RefreshToken { get; set; }
}
