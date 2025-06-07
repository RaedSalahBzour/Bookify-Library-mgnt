using Application.Users.Commands;
using Application.Users.Dtos;
using Application.Users.Services;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Users.Handlers
{
    internal class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<UserDto>>
    {
        private readonly IAuthService _authService;

        public DeleteUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<Result<UserDto>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _authService.DeleteUserAsync(command.id);
            if (!result.IsSuccess)
                return Result<UserDto>.Fail
                    (ErrorMessages.OperationFailed(nameof(OperationNames.DeleteUser),
                    result.Errors));
            return Result<UserDto>.Ok(result.Data);
        }
    }
}
