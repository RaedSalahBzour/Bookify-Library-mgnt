using Application.Authorization.Commands.Roles;
using Application.Authorization.Dtos.Roles;
using Application.Authorization.Services;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authorization.Handlers.Roles.CommandHandlers;

public class CreateRoleCommandHandler(IRoleService roleService, IMapper mapper)
    : IRequestHandler<CreateRoleCommand, IdentityRole>
{
    public async Task<IdentityRole> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
    {
        CreateRoleDto createRoleDto = mapper.Map<CreateRoleDto>(command);
        return await roleService.CreateRoleAsync(createRoleDto);

    }
}
