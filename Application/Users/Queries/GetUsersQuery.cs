using Application.Users.Dtos;
using MediatR;

namespace Application.Users.Queries
{
    public record GetUsersQuery() : IRequest<List<UserDto>>;
}
