using Application.Authorization.Commands.Roles;
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

namespace Application.Authorization.Handlers.Roles
{
    public class RemoveFromRoleCommandHanlder : IRequestHandler<RemoveFromRoleCommand, Result<string>>
    {
        private readonly IRoleService _roleService;

        public RemoveFromRoleCommandHanlder(IRoleService roleService)
        {
            _roleService = roleService;

        }
        public async Task<Result<string>> Handle(RemoveFromRoleCommand command, CancellationToken cancellationToken)
        {
            var result = await _roleService.RemoveUserFromRoleAsync(command.userId, command.roleName);
            if (!result.IsSuccess)
                return Result<string>.Fail(ErrorMessages.
                    OperationFailed(nameof(OperationNames.RemoveUserFromRole), result.Errors));
            return Result<string>.Ok(result.Data);
        }
    }
}
