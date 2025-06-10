using Application.Authorization.Dtos.Roles;
using Application.Authorization.Queries.Roles;
using Application.Authorization.Services;
using MediatR;

namespace Application.Authorization.Handlers.Roles.QeuryHandlers;

public class GetRoleByIdQueryHandler(IRoleService roleService)
    : IRequestHandler<GetRoleByIdQuery, RoleDto>
{

    public async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {

        return await roleService.GetRoleByIdAsync(request.id);

    }
}
