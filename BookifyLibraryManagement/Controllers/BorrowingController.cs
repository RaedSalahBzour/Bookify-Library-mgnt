using Application.Borrowings.Commands;
using Application.Borrowings.Dtos;
using Application.Borrowings.Queries;
using Application.Borrowings.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace Bookify_Library_mgnt.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BorrowingController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetBorrowings()
    {
        List<BorrowingDto> result = await _sender.Send(new GetBorrowingsQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBorrowingById([FromRoute] string id)
    {
        BorrowingDto result = await _sender.Send(new GetBorrowingByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBorrowing([FromBody] CreateBorrowingCommand command)
    {
        BorrowingDto result = await _sender.Send(command);
        return CreatedAtAction(nameof(GetBorrowingById), new { Id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateBorrowing([FromRoute] string id, [FromBody] UpdateBorrowingCommand command)
    {
        command.Id = id;
        BorrowingDto result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteBorrowing([FromRoute] string id)
    {
        BorrowingDto result = await _sender.Send(new DeleteBorrowingCommand { Id = id });
        return Ok(result);
    }
}
