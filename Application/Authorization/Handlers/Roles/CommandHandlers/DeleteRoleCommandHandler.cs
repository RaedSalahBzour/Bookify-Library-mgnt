using Application.Authorization.Commands.Roles;
using Application.Authorization.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authorization.Handlers.Roles.CommandHandlers;

public class DeleteRoleCommandHandler(IRoleService roleService)
    : IRequestHandler<DeleteRoleCommand, IdentityRole>
{
    public async Task<IdentityRole> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
    {
        return await roleService.DeleteRoleAsync(command.Id);

    }
}


