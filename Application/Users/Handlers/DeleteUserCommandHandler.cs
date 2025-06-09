using Application.Users.Commands;
using Application.Users.Dtos;
using Application.Users.Services;
using MediatR;

namespace Application.Users.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, UserDto>
    {
        private readonly IAuthService _authService;

        public DeleteUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<UserDto> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            return await _authService.DeleteUserAsync(command.Id);

        }
    }
}
