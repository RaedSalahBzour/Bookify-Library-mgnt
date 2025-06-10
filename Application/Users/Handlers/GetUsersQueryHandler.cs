using Application.Users.Dtos;
using Application.Users.Queries;
using Application.Users.Services;
using MediatR;

namespace Application.Users.Handlers;

public class GetUsersQueryHandler(IAuthService authService)
    : IRequestHandler<GetUsersQuery, List<UserDto>>
{
    public async Task<List<UserDto>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        return await authService.GetUsersAsync();

    }
}
