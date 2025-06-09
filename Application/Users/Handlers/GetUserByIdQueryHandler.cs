using Application.Users.Dtos;
using Application.Users.Queries;
using Application.Users.Services;
using MediatR;

namespace Application.Users.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IAuthService _authService;

        public GetUserByIdQueryHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<UserDto> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            return await _authService.GetUserByIdAsync(query.id);

        }
    }
}
