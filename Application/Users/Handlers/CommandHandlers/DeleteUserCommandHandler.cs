using Application.Users.Commands;
using Application.Users.Dtos;
using Application.Users.Services;
using MediatR;

namespace Application.Users.Handlers.CommandHandlers;

public class DeleteUserCommandHandler(IAuthService authService)
    : IRequestHandler<DeleteUserCommand, UserDto>
{
    public async Task<UserDto> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        return await authService.DeleteUserAsync(command.Id);

    }
}
