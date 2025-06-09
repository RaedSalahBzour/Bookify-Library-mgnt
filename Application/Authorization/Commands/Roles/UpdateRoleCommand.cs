using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Authorization.Commands.Roles
{
    public class UpdateRoleCommand : IRequest<IdentityRole>
    {
        [JsonIgnore]
        public string? id { get; set; }
        [Required(ErrorMessage = "Role Name is Required")]
        public string RoleName { get; set; }

    }
}
