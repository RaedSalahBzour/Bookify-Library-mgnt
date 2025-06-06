using Application.Authorization.Commands.Roles;
using Application.Authorization.Dtos.Roles;
using Application.Authorization.Handlers.Roles;
using Application.Authorization.Services;
using AutoMapper;
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
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Result<IdentityRole>>
    {
        private readonly IRoleService _roleService;

        public DeleteRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public async Task<Result<IdentityRole>> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
        {
            var result = await _roleService.DeleteRoleAsync(command.id);
            if (!result.IsSuccess)
                return Result<IdentityRole>.Fail(ErrorMessages.
                    OperationFailed(nameof(OperationNames.DeleteRole), result.Errors));
            return Result<IdentityRole>.Ok(result.Data);
        }
    }
}


