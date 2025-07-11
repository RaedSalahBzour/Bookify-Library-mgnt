﻿using Application.Users.Commands;
using Application.Users.Dtos;
using Application.Users.Services;
using AutoMapper;
using MediatR;

namespace Application.Users.Handlers.CommandHandlers;

public class CreateUserCommandHandler(IAuthService authService, IMapper mapper)
    : IRequestHandler<CreateUserCommand, UserDto>
{
    public async Task<UserDto> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        CreateUserDto createUserDto = mapper.Map<CreateUserDto>(command);
        return await authService.CreateAsync(createUserDto);

    }
}
