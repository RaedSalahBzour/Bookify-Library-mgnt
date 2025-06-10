using Application.Users.Dtos;
using MediatR;

namespace Application.Users.Queries;

public record GetUserByIdQuery(string id) : IRequest<UserDto>;
