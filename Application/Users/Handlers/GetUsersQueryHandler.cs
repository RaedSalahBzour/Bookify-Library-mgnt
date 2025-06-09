using Application.Users.Dtos;
using Application.Users.Queries;
using Application.Users.Services;
using MediatR;

namespace Application.Users.Handlers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
    {
        private readonly IAuthService _authService;

        public GetUsersQueryHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<List<UserDto>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
        {
            return await _authService.GetUsersAsync();

        }
    }
}
