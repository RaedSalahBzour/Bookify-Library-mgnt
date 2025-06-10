using System.ComponentModel.DataAnnotations;

namespace Application.Authorization.Dtos.Claims;

public class AddClaimToUserDto
{
    [Required(ErrorMessage = "User Id is required")]
    public string userId { get; set; }
    [Required(ErrorMessage = "Claim Type is required")]
    [MinLength(3)]
    public string claimType { get; set; }
    [Required(ErrorMessage = "Claim Value is required")]
    [MinLength(2)]
    public string claimValue { get; set; }
}
