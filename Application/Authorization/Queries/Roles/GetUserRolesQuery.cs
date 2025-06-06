using Application.Authorization.Dtos.Roles;
using Bookify_Library_mgnt.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authorization.Queries.Roles
{
    public record GetUserRolesQuery(string email) : IRequest<Result<IEnumerable<string>>>;

}
