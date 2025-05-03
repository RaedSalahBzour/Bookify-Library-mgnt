using Bookify_Library_mgnt.Dtos.Reviews;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookify_Library_mgnt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetReviews()
        {
            return Ok(await _reviewRepository.GetReviewsAsync());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetReviewById(string id)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }


        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto reviewDto)
        {
            if (reviewDto == null)
            {
                return BadRequest("provaide a valid Review");
            }
            var review = await _reviewRepository.CreateReviewAsync(reviewDto);
            return CreatedAtAction(nameof(GetReviewById), new { id = review.Id }, reviewDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateReview([FromRoute] string id, [FromBody] UpdateReviewDto reviewDto)
        {
            if (reviewDto == null)
            {
                return BadRequest("provaide a valid Review");
            }
            var review = await _reviewRepository.UpdateReviewAsync(id, reviewDto);
            return Ok(reviewDto);

        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteReview(string id)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            await _reviewRepository.DeleteReviewAsync(id);
            return NoContent();
        }
    }
}
