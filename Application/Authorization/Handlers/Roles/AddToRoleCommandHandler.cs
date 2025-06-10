using Application.Authorization.Commands.Roles;
using Application.Authorization.Services;
using MediatR;

namespace Application.Authorization.Handlers.Roles;

public class AddToRoleCommandHandler : IRequestHandler<AddToRoleCommand, string>
{
    private readonly IRoleService _roleService;

    public AddToRoleCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;

    }
    public async Task<string> Handle(AddToRoleCommand command, CancellationToken cancellationToken)
    {
        return await _roleService.AddUserToRoleAsync(command.UserId, command.RoleName);

    }
}
