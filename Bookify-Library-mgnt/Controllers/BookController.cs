using Bookify_Library_mgnt.Dtos.Books;
using Bookify_Library_mgnt.Models;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Bookify_Library_mgnt.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace Bookify_Library_mgnt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
            {
                return BadRequest("not found");
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDto bookDto)
        {
            var book = await _bookService.CreateBookAsync(bookDto);
            return CreatedAtAction(nameof(GetBookById), new { Id = book.Id }, bookDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateBook([FromRoute] string id, [FromBody] UpdateBookDto bookDto)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book is null)
            {
                return BadRequest("not found");
            }
            await _bookService.UpdateBookAsync(id, bookDto);
            return Ok(bookDto);


        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book is null)
            {
                return BadRequest("not found");
            }
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }
    }
}
