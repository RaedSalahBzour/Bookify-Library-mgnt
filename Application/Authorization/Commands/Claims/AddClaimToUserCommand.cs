using Bookify_Library_mgnt.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authorization.Commands.Claims
{
    public record AddClaimToUserCommand : IRequest<Result<string>>
    {
        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; init; }

        [Required(ErrorMessage = "Claim type is required")]
        public string ClaimType { get; init; }

        [Required(ErrorMessage = "Claim value is required")]
        public string ClaimValue { get; init; }
    }

}
