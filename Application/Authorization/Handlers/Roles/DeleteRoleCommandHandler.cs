using Application.Authorization.Commands.Roles;
using Application.Authorization.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authorization.Handlers.Roles
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, IdentityRole>
    {
        private readonly IRoleService _roleService;

        public DeleteRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public async Task<IdentityRole> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
        {
            return await _roleService.DeleteRoleAsync(command.Id);

        }
    }
}


