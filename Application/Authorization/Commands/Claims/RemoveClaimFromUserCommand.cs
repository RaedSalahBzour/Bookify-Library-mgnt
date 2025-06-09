using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Authorization.Commands.Claims
{
    public record RemoveClaimFromUserCommand(string UserId, string ClaimType) : IRequest<string>
    {
        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; init; }

        [Required(ErrorMessage = "Claim type is required")]
        public string ClaimType { get; init; }
    }
}
