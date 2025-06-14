﻿using Application.Books.Commands;
using Application.Books.Dtos;
using Application.Books.Queries;
using Application.Books.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Bookify_Library_mgnt.Controllers;

[Route("api/[controller]")]
[ApiController()]
public class BookController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        List<BookDto> books = await _sender.Send(new GetBooksQuery());
        return Ok(books);
    }

    [HttpGet("{id:guid}")]

    public async Task<IActionResult> GetBookById([FromRoute] string id)
    {
        BookDto result = await _sender.Send(new GetBookByIdQuery(id));
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookCommand command)
    {
        BookDto result = await _sender.Send(command);

        return CreatedAtAction(nameof(GetBookById), new { Id = result.Id }, result);
    }
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateBook([FromRoute] string id, [FromBody] UpdateBookCommand command)
    {
        command.Id = id;
        BookDto result = await _sender.Send(command);
        return Ok(result);


    }

    [HttpDelete("{id:guid}")]
    [Authorize(policy: "CanDeleteBookPolicy")]

    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        BookDto result = await _sender.Send(new DeleteBookCommand { Id = id });

        return Ok(result);
    }
}
