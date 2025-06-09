using Application.Authorization.Commands.Auth;
using Application.Authorization.Dtos.Token;
using Application.Users.Dtos;
using Application.Users.Services;
using AutoMapper;
using MediatR;

namespace Application.Authorization.Handlers.Auth
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, TokenResponseDto>
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public LoginCommandHandler(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<TokenResponseDto> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<LoginDto>(command);
            return await _authService.LoginAsync(dto);

        }
    }
}
