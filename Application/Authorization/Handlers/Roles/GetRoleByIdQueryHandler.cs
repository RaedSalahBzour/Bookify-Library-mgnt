using Application.Authorization.Dtos.Roles;
using Application.Authorization.Queries.Roles;
using Application.Authorization.Services;
using Bookify_Library_mgnt.Common;
using Domain.Enums;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authorization.Handlers.Roles
{
    internal class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Result<RoleDto>>
    {
        private readonly IRoleService _roleService;

        public GetRoleByIdQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;

        }

        public async Task<Result<RoleDto>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {

            var result = await _roleService.GetRoleByIdAsync(request.id);
            if (!result.IsSuccess)
                return Result<RoleDto>.Fail(ErrorMessages.
                    OperationFailed(nameof(OperationNames.GetRole), result.Errors));
            return Result<RoleDto>.Ok(result.Data);
        }
    }
}
