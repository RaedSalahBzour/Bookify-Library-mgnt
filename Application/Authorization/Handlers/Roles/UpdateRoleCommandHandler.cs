using Application.Authorization.Commands.Roles;
using Application.Authorization.Dtos.Roles;
using Application.Authorization.Services;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authorization.Handlers.Roles
{
    internal class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, IdentityRole>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public UpdateRoleCommandHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }
        public async Task<IdentityRole> Handle(UpdateRoleCommand command, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<UpdateRoleDto>(command);
            return await _roleService.UpdateRoleAsync(command.id, dto);

        }
    }
}
