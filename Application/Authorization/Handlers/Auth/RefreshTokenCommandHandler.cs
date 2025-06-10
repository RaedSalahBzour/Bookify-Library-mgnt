using Application.Authorization.Commands.Auth;
using Application.Authorization.Dtos.Token;
using Application.Users.Services;
using AutoMapper;
using MediatR;

namespace Application.Authorization.Handlers.Auth;

public class RefreshTokenCommandHandler(IAuthService authService, IMapper mapper)
    : IRequestHandler<RefreshTokenCommand, TokenResponseDto>
{
    public async Task<TokenResponseDto> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        RefreshTokenRequestDto refreshTokenRequestDto = mapper.Map<RefreshTokenRequestDto>(command);
        return await authService.RefreshTokenAsync(refreshTokenRequestDto);

    }
}
