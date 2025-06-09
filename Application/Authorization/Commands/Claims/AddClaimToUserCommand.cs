using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Authorization.Commands.Claims
{
    public record AddClaimToUserCommand : IRequest<string>
    {
        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; init; }

        [Required(ErrorMessage = "Claim type is required")]
        public string ClaimType { get; init; }

        [Required(ErrorMessage = "Claim value is required")]
        public string ClaimValue { get; init; }
    }

}
