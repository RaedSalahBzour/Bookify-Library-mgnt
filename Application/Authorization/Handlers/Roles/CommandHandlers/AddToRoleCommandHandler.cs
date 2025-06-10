using Application.Authorization.Commands.Roles;
using Application.Authorization.Services;
using MediatR;

namespace Application.Authorization.Handlers.Roles.CommandHandlers;

public class AddToRoleCommandHandler(IRoleService roleService)
    : IRequestHandler<AddToRoleCommand, string>
{
    public async Task<string> Handle(AddToRoleCommand command, CancellationToken cancellationToken)
    {
        return await roleService.AddUserToRoleAsync(command.UserId, command.RoleName);

    }
}
