﻿using Application.Authorization.Commands.Auth;
using Application.Authorization.Dtos.Token;
using Application.Users.Commands;
using Application.Users.Dtos;
using Application.Users.Queries;
using Application.Users.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace Bookify_Library_mgnt.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "admin")]
public class AuthController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        return Ok(await _sender.Send(new GetUsersQuery()));

    }

    [HttpGet("users/{id:guid}")]
    public async Task<IActionResult> GetUserById([FromRoute] string id)
    {
        UserDto result = await _sender.Send(new GetUserByIdQuery(id));
        return Ok(result);
    }
    [AllowAnonymous]
    [HttpPost("users")]
    public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
    {
        UserDto result = await _sender.Send(command);
        return CreatedAtAction(nameof(GetUserById), new { Id = result.Id }, result);
    }

    [HttpPut("users/{id:guid}")]
    [Authorize(Roles = "superAdmin")]
    public async Task<IActionResult> UpdateUser([FromRoute] string id, [FromBody] UpdateUserCommand command)
    {
        command.id = id;
        UserDto result = await _sender.Send(command);
        return Ok(result);

    }
    [HttpDelete("users/{id:guid}")]
    [Authorize(Roles = "superAdmin")]
    public async Task<IActionResult> DeleteUser([FromRoute] string id)
    {
        UserDto result = await _sender.Send(new DeleteUserCommand { Id = id });
        return Ok(result);
    }
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        TokenResponseDto result = await _sender.Send(command);
        return Ok(result);
    }
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefrshToken([FromBody] RefreshTokenCommand command)
    {
        TokenResponseDto result = await _sender.Send(command);
        return Ok(result);
    }


}
