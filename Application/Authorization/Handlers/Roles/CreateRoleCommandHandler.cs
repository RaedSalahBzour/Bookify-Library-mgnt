using Application.Authorization.Commands.Roles;
using Application.Authorization.Dtos.Roles;
using Application.Authorization.Services;
using Application.Books.Dtos;
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
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result<IdentityRole>>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public CreateRoleCommandHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        public async Task<Result<IdentityRole>> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<CreateRoleDto>(command);
            var result = await _roleService.CreateRoleAsync(dto);
            if (!result.IsSuccess)
                return Result<IdentityRole>.Fail(ErrorMessages.
                    OperationFailed(nameof(OperationNames.CreateRole), result.Errors));
            return Result<IdentityRole>.Ok(result.Data);
        }
    }
}
