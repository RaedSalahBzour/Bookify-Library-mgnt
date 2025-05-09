using Bookify_Library_mgnt.Dtos.Reviews;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Bookify_Library_mgnt.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookify_Library_mgnt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReviews(int pageNumber = 1, int pageSize = 10)
        {
            return Ok(await _reviewService.GetReviewsAsync(pageNumber, pageSize));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetReviewById(string id)
        {
            var result = await _reviewService.GetReviewByIdAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto reviewDto)
        {
            var result = await _reviewService.CreateReviewAsync(reviewDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return CreatedAtAction(nameof(GetReviewById), new { id = result.Data.Id }, reviewDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateReview([FromRoute] string id, [FromBody] UpdateReviewDto reviewDto)
        {

            var result = await _reviewService.UpdateReviewAsync(id, reviewDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result);

        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteReview([FromRoute] string id)
        {
            var result = await _reviewService.GetReviewByIdAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            await _reviewService.DeleteReviewAsync(id);
            return NoContent();
        }
    }
}
