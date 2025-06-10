using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Authorization.Commands.Roles;

public class AddToRoleCommand : IRequest<string>
{
    [Required(ErrorMessage = "User ID is required.")]
    public string UserId { get; init; }

    [Required(ErrorMessage = "Role name is required.")]
    public string RoleName { get; init; }
}
