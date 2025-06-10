using Application.Authorization.Commands.Auth;
using Application.Authorization.Dtos.Token;
using Application.Users.Dtos;
using Application.Users.Services;
using AutoMapper;
using MediatR;

namespace Application.Authorization.Handlers.Auth;

public class LoginCommandHandler(IAuthService authService, IMapper mapper)
    : IRequestHandler<LoginCommand, TokenResponseDto>
{
    public async Task<TokenResponseDto> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<LoginDto>(command);
        return await authService.LoginAsync(dto);

    }
}
