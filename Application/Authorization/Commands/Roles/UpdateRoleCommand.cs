using Bookify_Library_mgnt.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Application.Authorization.Commands.Roles
{
    public class UpdateRoleCommand : IRequest<Result<IdentityRole>>
    {
        [JsonIgnore]
        public string? id { get; set; }
        public string RoleName { get; set; }

    }
}
