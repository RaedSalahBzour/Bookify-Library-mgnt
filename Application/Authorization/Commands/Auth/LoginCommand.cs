using Application.Authorization.Dtos.Token;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Authorization.Commands.Auth
{
    public class LoginCommand : IRequest<TokenResponseDto?>
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
