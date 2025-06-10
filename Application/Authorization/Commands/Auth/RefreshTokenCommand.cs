using Application.Authorization.Dtos.Token;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Authorization.Commands.Auth;

public class RefreshTokenCommand : IRequest<TokenResponseDto>
{
    [Required(ErrorMessage = "User Id is required")]
    public string UserId { get; set; }
    [Required(ErrorMessage = "Refresh Token is required")]
    public required string RefreshToken { get; set; }
}
