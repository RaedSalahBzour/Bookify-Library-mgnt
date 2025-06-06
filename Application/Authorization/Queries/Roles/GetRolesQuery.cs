using Application.Authorization.Dtos.Roles;
using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Helper.Pagination;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authorization.Queries.Roles
{
    public record GetRolesQuery(int pageNumber = 1, int pageSize = 10) : IRequest<PagedResult<RoleDto>>;

}
