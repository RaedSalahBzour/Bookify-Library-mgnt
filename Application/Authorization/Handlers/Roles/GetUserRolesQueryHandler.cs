using Application.Authorization.Commands.Roles;
using Application.Authorization.Handlers.Roles;
using Application.Authorization.Queries.Roles;
using Application.Authorization.Services;
using Bookify_Library_mgnt.Common;
using Domain.Enums;
using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Authorization.Handlers.Roles
{

    public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, Result<IEnumerable<string>>>
    {
        private readonly IRoleService _roleService;

        public GetUserRolesQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;

        }
        public async Task<Result<IEnumerable<string>>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            var result = await _roleService.GetUserRoles(request.email);
            if (!result.IsSuccess)
                return Result<IEnumerable<string>>.Fail(ErrorMessages.
                    OperationFailed(nameof(OperationNames.GetUserRoles), result.Errors));
            return Result<IEnumerable<string>>.Ok(result.Data);
        }
    }
}

