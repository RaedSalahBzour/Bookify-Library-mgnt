using Application.Users.Commands;
using Application.Users.Dtos;
using Application.Users.Services;
using AutoMapper;
using MediatR;

namespace Application.Users.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }
        public async Task<UserDto> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<UpdateUserDto>(command);
            return await _authService.UpdateUserAsync(command.id, dto);

        }
    }
}
