using Application.Authorization.Commands.Roles;
using Application.Authorization.Dtos.Roles;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Authorization.Handlers.Roles
{
    internal class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Result<IdentityRole>>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public UpdateRoleCommandHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }
        public async Task<Result<IdentityRole>> Handle(UpdateRoleCommand command, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<UpdateRoleDto>(command);
            var result = await _roleService.UpdateRoleAsync(command.id, dto);
            if (!result.IsSuccess)
                return Result<IdentityRole>.Fail(ErrorMessages.
                    OperationFailed(nameof(OperationNames.UpdateRole), result.Errors));
            return Result<IdentityRole>.Ok(result.Data);
        }
    }
}
