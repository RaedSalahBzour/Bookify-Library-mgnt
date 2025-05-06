using Bookify_Library_mgnt.Dtos.Borrowings;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Bookify_Library_mgnt.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Bookify_Library_mgnt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        private readonly IBorrowingService _borrowingService;

        public BorrowingController(IBorrowingService borrowingService)
        {
            _borrowingService = borrowingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBorrowings(int pageNumber = 1, int pageSize = 10)
        {
            return Ok(await _borrowingService.GetBorrowingsAsync(pageNumber, pageSize));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetBorrowingById([FromRoute] string id)
        {
            var borrowing = await _borrowingService.GetBorrowingByIdAsync(id);
            if (borrowing == null) { return NotFound(); }
            return Ok(borrowing);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBorrowing([FromBody] CreateBorrowingDto borrowingDto)
        {
            var borrowing = await _borrowingService.CreateBorrowingAsync(borrowingDto);
            return CreatedAtAction(nameof(GetBorrowingById), new { Id = borrowing.Id }, borrowingDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateBorrowing([FromRoute] string id, [FromBody] UpdateBorrowingDto borrowingDto)
        {
            var borrowing = await _borrowingService.GetBorrowingByIdAsync(id);
            if (borrowing == null) { return NotFound(); }
            await _borrowingService.UpdateBorrowingAsync(id, borrowingDto);
            return Ok(borrowingDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBorrowing([FromRoute] string id)
        {
            var borrowing = await _borrowingService.GetBorrowingByIdAsync(id);
            if (borrowing == null) { return NotFound(); }
            await _borrowingService.DeleteBorrowingAsync(id);
            return NoContent();
        }
    }
}
