using Application.Users.Dtos;
using Application.Users.Queries;
using Application.Users.Services;
using MediatR;

namespace Application.Users.Handlers.QeuryHandlers;

public class GetUserByIdQueryHandler(IAuthService authService)
    : IRequestHandler<GetUserByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        return await authService.GetUserByIdAsync(query.id);

    }
}
