using Application.Authorization.Commands.Roles;
using Application.Authorization.Services;
using AutoMapper;
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
    public class AddToRoleCommandHandler : IRequestHandler<AddToRoleCommand, Result<string>>
    {
        private readonly IRoleService _roleService;

        public AddToRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;

        }
        public async Task<Result<string>> Handle(AddToRoleCommand command, CancellationToken cancellationToken)
        {
            var result = await _roleService.AddUserToRoleAsync(command.userId, command.roleName);
            if (!result.IsSuccess)
                return Result<string>.Fail(ErrorMessages.
                    OperationFailed(nameof(OperationNames.AddUserToRole), result.Errors));
            return Result<string>.Ok(result.Data);
        }
    }
}
