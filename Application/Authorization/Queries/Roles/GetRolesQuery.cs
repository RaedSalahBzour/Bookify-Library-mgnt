using Application.Authorization.Dtos.Roles;
using MediatR;

namespace Application.Authorization.Queries.Roles
{
    public record GetRolesQuery() : IRequest<List<RoleDto>>;

}
