using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Dtos.Books;
using Bookify_Library_mgnt.Models;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Bookify_Library_mgnt.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace Bookify_Library_mgnt.Controllers
{
    [Route("api/[controller]")]
    [ApiController()]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks(int pageNumber = 1, int pageSize = 10,
                    string? title = null,
                    string? category = null,
                   DateOnly? publishtDate = null,
                   string? sortBy = null,
                   bool descending = false)
        {
            var books = await _bookService.GetBooksAsync(pageNumber, pageSize, title, category, publishtDate, sortBy, descending);
            return Ok(books);
        }

        [HttpGet("{id:guid}")]

        public async Task<IActionResult> GetBookById([FromRoute] string id)
        {
            var result = await _bookService.GetByIdAsync(id);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDto bookDto)
        {
            var result = await _bookService.CreateBookAsync(bookDto);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return CreatedAtAction(nameof(GetBookById), new { Id = result.Data.Id }, bookDto);
        }
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateBook([FromRoute] string id, [FromBody] UpdateBookDto bookDto)
        {
            var result = await _bookService.UpdateBookAsync(id, bookDto);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return Ok(bookDto);


        }

        [HttpDelete("{id:guid}")]
        [Authorize(policy: "CanDeleteBookPolicy")]

        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return NoContent();
        }
    }
}
