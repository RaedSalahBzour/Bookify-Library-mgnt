using Application.Authorization.Commands.Roles;
using Application.Authorization.Services;
using MediatR;

namespace Application.Authorization.Handlers.Roles.CommandHandlers;

public class RemoveFromRoleCommandHanlder(IRoleService roleService)
    : IRequestHandler<RemoveFromRoleCommand, string>
{
    public async Task<string> Handle(RemoveFromRoleCommand command, CancellationToken cancellationToken)
    {
        return await roleService.RemoveUserFromRoleAsync(command.UserId, command.RoleName);

    }
}
