using Bookify_Library_mgnt.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authorization.Commands.Roles
{
    public record CreateRoleCommand(string RoleName) : IRequest<Result<IdentityRole>>;

}
