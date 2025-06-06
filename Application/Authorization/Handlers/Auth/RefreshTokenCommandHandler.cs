using Application.Authorization.Commands.Auth;
using Application.Authorization.Dtos.Token;
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
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<TokenResponseDto?>>
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public RefreshTokenCommandHandler(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<Result<TokenResponseDto?>> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<RefreshTokenRequestDto>(command);
            var result = await _authService.RefreshTokenAsync(dto);
            if (!result.IsSuccess)
                return Result<TokenResponseDto?>.Fail(ErrorMessages.
                    OperationFailed(nameof(OperationNames.RefreshToken), result.Errors));
            return Result<TokenResponseDto?>.Ok(result.Data);
        }
    }
}
