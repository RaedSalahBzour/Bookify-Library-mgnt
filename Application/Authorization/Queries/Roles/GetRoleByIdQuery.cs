using Application.Authorization.Dtos.Roles;
using MediatR;

namespace Application.Authorization.Queries.Roles
{
    public record GetRoleByIdQuery(string id) : IRequest<RoleDto>;


}
