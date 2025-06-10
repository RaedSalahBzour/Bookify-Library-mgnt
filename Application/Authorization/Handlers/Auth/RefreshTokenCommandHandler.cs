using Application.Authorization.Commands.Auth;
using Application.Authorization.Dtos.Token;
using Application.Users.Services;
using AutoMapper;
using MediatR;

namespace Application.Authorization.Handlers.Auth;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenResponseDto>
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;
    public RefreshTokenCommandHandler(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<TokenResponseDto> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<RefreshTokenRequestDto>(command);
        return await _authService.RefreshTokenAsync(dto);

    }
}
