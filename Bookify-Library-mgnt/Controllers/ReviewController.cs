using Application.Reviews.Commands;
using Application.Reviews.Dtos;
using Application.Reviews.Queries;
using Application.Reviews.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Bookify_Library_mgnt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ISender _sender;

        public ReviewController(ISender sender)
        {
            _sender = sender;
        }


        [HttpGet]
        public async Task<IActionResult> GetReviews(int pageNumber = 1, int pageSize = 10)
        {
            return Ok(await _sender.Send(new GetReviewsQuery(pageNumber, pageSize)));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetReviewById(string id)
        {
            var result = await _sender.Send(new GetReviewByIdQuery(id));
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }
        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewCommand command)
        {
            var result = await _sender.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return CreatedAtAction(nameof(GetReviewById), new { id = result.Data.Id }, result.Data);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateReview([FromRoute] string id, [FromBody] UpdateReviewCommand command)
        {
            command.id = id;
            var result = await _sender.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);

        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteReview([FromRoute] string id)
        {
            var result = await _sender.Send(new DeleteReviewCommand(id));
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return NoContent();
        }
    }
}
