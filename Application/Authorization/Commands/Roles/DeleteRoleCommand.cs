using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Application.Authorization.Commands.Roles
{
    public class DeleteRoleCommand : IRequest<IdentityRole>
    {
        [Required(ErrorMessage = "Role ID is required.")]
        public string Id { get; init; }
    }
}
