using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Application.Authorization.Commands.Roles
{
    public record DeleteRoleCommand(string Id) : IRequest<IdentityRole>
    {
        [Required(ErrorMessage = "Role ID is required.")]
        public string Id { get; init; }
    }
}
