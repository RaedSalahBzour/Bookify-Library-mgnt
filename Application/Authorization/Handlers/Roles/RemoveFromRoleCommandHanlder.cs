using Application.Authorization.Commands.Roles;
using Application.Authorization.Services;
using MediatR;

namespace Application.Authorization.Handlers.Roles;

public class RemoveFromRoleCommandHanlder : IRequestHandler<RemoveFromRoleCommand, string>
{
    private readonly IRoleService _roleService;

    public RemoveFromRoleCommandHanlder(IRoleService roleService)
    {
        _roleService = roleService;

    }
    public async Task<string> Handle(RemoveFromRoleCommand command, CancellationToken cancellationToken)
    {
        return await _roleService.RemoveUserFromRoleAsync(command.UserId, command.RoleName);

    }
}
