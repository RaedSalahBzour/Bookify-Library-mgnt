using Application.Authorization.Dtos.Roles;
using Application.Authorization.Queries.Roles;
using Application.Authorization.Services;
using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Helper.Pagination;
using Domain.Enums;
using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authorization.Handlers.Roles
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, PagedResult<RoleDto>>
    {
        private readonly IRoleService _roleService;

        public GetRolesQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;

        }
        public async Task<PagedResult<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var result = await _roleService.GetRolesAsync(request.pageNumber, request.pageSize);

            return new PagedResult<RoleDto>
            {
                PageNumber = request.pageNumber,
                PageSize = request.pageSize,
                Items = result.Items,
                TotalCount = result.TotalCount,
            };
        }
    }
}
