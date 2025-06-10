using Application.Authorization.Commands.Roles;
using Application.Authorization.Dtos.Roles;
using Application.Authorization.Services;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authorization.Handlers.Roles.CommandHandlers;

internal class UpdateRoleCommandHandler(IRoleService roleService, IMapper mapper)
    : IRequestHandler<UpdateRoleCommand, IdentityRole>
{
    public async Task<IdentityRole> Handle(UpdateRoleCommand command, CancellationToken cancellationToken)
    {
        UpdateRoleDto updateRoleDto = mapper.Map<UpdateRoleDto>(command);
        return await roleService.UpdateRoleAsync(command.id, updateRoleDto);

    }
}
