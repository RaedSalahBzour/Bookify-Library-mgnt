using Application.Users.Commands;
using Application.Users.Dtos;
using Application.Users.Services;
using AutoMapper;
using MediatR;

namespace Application.Users.Handlers.CommandHandlers;

public class UpdateUserCommandHandler(IAuthService authService, IMapper mapper)
    : IRequestHandler<UpdateUserCommand, UserDto>
{
    public async Task<UserDto> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        UpdateUserDto updateUserDto = mapper.Map<UpdateUserDto>(command);
        return await authService.UpdateUserAsync(command.id, updateUserDto);

    }
}
