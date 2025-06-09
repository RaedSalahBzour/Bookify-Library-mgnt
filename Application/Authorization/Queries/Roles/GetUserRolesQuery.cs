using MediatR;

namespace Application.Authorization.Queries.Roles
{
    public record GetUserRolesQuery(string email) : IRequest<IList<string>>;

}
