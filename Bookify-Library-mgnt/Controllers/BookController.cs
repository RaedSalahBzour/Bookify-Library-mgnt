using Bookify_Library_mgnt.Dtos;
using Bookify_Library_mgnt.Models;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace Bookify_Library_mgnt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookRepository.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return BadRequest("not found");
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDto bookDto)
        {
            var book = await _bookRepository.CreateBookAsync(bookDto);
            return CreatedAtAction(nameof(GetById), new { Id = book.Id }, bookDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateBook([FromRoute] string id, [FromBody] UpdateBookDto bookDto)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book is null)
            {
                return BadRequest("not found");
            }
            await _bookRepository.UpdateBookAsync(id, bookDto);
            return Ok(bookDto);


        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book is null)
            {
                return BadRequest("not found");
            }
            await _bookRepository.DeleteBookAsync(id);
            return NoContent();
        }
    }
}
