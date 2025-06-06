using Application.Authorization.Commands.Auth;
using Application.Authorization.Dtos.Claims;
using Application.Authorization.Dtos.Token;
using Application.Authorization.Services;
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

namespace Application.Authorization.Handlers.Auth
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<TokenResponseDto?>>
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public LoginCommandHandler(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<Result<TokenResponseDto?>> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<LoginDto>(command);
            var result = await _authService.LoginAsync(dto);
            if (!result.IsSuccess)
                return Result<TokenResponseDto?>.Fail(ErrorMessages.
                    OperationFailed(nameof(OperationNames.Login), result.Errors));
            return Result<TokenResponseDto?>.Ok(result.Data);
        }
    }
}
