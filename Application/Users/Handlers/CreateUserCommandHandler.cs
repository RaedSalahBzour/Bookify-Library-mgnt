using Application.Reviews.Dtos;
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

namespace Application.Users.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserDto>>
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<Result<UserDto>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<CreateUserDto>(command);
            var result = await _authService.CreateAsync(dto);
            if (!result.IsSuccess)
                return Result<UserDto>.Fail
                    (ErrorMessages.OperationFailed(nameof(OperationNames.CreateUser),
                    result.Errors));
            return Result<UserDto>.Ok(result.Data);
        }
    }
}
