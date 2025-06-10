using Application.Authorization.Queries.Roles;
using Application.Authorization.Services;
using MediatR;

namespace Application.Authorization.Handlers.Roles;


public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, IList<string>>
{
    private readonly IRoleService _roleService;

    public GetUserRolesQueryHandler(IRoleService roleService)
    {
        _roleService = roleService;

    }
    public async Task<IList<string>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
    {
        return await _roleService.GetUserRoles(request.email);

    }
}

