using Application.Authorization.Commands.Roles;
using Application.Authorization.Dtos.Roles;
using Application.Authorization.Services;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authorization.Handlers.Roles
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, IdentityRole>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public CreateRoleCommandHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        public async Task<IdentityRole> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<CreateRoleDto>(command);
            return await _roleService.CreateRoleAsync(dto);

        }
    }
}
