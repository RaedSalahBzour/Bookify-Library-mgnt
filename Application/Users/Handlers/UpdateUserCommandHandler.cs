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
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<UserDto>>
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }
        public async Task<Result<UserDto>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<UpdateUserDto>(command);
            var result = await _authService.UpdateUserAsync(command.id, dto);
            if (!result.IsSuccess)
                return Result<UserDto>.Fail
                    (ErrorMessages.OperationFailed(nameof(OperationNames.UpdateUser),
                    result.Errors));
            return Result<UserDto>.Ok(result.Data);
        }
    }
}
