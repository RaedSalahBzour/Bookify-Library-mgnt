using Application.Users.Commands;
using Application.Users.Dtos;
using Application.Users.Services;
using AutoMapper;
using MediatR;

namespace Application.Users.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<CreateUserDto>(command);
            return await _authService.CreateAsync(dto);

        }
    }
}
