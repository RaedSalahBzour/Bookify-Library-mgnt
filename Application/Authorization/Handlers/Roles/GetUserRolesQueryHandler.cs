using Application.Authorization.Queries.Roles;
using Application.Authorization.Services;
using MediatR;

namespace Application.Authorization.Handlers.Roles;


public class GetUserRolesQueryHandler(IRoleService roleService)
    : IRequestHandler<GetUserRolesQuery, IList<string>>
{
    public async Task<IList<string>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
    {
        return await roleService.GetUserRoles(request.email);

    }
}

