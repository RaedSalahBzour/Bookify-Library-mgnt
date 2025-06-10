using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Application.Authorization.Commands.Roles;

public record CreateRoleCommand : IRequest<IdentityRole>
{
    [Required(ErrorMessage = "Role name is required.")]
    [MinLength(3, ErrorMessage = "Role name must be at least 3 characters.")]
    public string RoleName { get; init; }
}
