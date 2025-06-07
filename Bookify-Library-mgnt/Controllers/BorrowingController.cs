using Application.Borrowings.Commands;
using Application.Borrowings.Dtos;
using Application.Borrowings.Queries;
using Application.Borrowings.Services;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookify_Library_mgnt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        private readonly IBorrowingService _borrowingService;
        private readonly ISender _sender;

        public BorrowingController(IBorrowingService borrowingService, ISender sender)
        {
            _borrowingService = borrowingService;
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetBorrowings(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _sender.Send(new GetBorrowingsQuery(pageNumber, pageSize));
            return Ok(result.Items);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetBorrowingById([FromRoute] string id)
        {
            var result = await _sender.Send(new GetBorrowingByIdQuery(id));
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBorrowing([FromBody] CreateBorrowingCommand command)
        {
            var result = await _sender.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return CreatedAtAction(nameof(GetBorrowingById), new { Id = result.Data.Id }, result.Data);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateBorrowing([FromRoute] string id, [FromBody] UpdateBorrowingCommand command)
        {
            command.Id = id;
            var result = await _sender.Send(command);
            if (!result.IsSuccess) { return BadRequest(result.Errors); }
            return Ok(result.Data);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteBorrowing([FromRoute] string id)
        {
            var result = await _sender.Send(new DeleteBorrowingCommand(id));
            if (!result.IsSuccess) { return BadRequest(result.Errors); }
            return NoContent();
        }
    }
}
