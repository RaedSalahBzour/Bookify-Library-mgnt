using Application.Categories.Commands;
using Application.Categories.Dtos;
using Application.Categories.Queries;
using Application.Categories.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Bookify_Library_mgnt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class CategoryController : ControllerBase
    {
        private readonly ISender _sender;

        public CategoryController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories(int pageNumber = 1, int pageSize = 10)
        {
            var categories = await _sender.Send(new GetCategoriesQuery(pageNumber, pageSize));
            return Ok(categories);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] string id)
        {
            var result = await _sender.Send(new GetCategoryByIdQuery(id));
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            var result = await _sender.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return CreatedAtAction(nameof(GetCategoryById), new { Id = result.Data.Id }, result.Data);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] string id, [FromBody] UpdateCategoryCommand command)
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
        [Authorize(Roles = "superAdmin")]
        public async Task<IActionResult> DeleteCategory([FromRoute] string id)
        {
            var result = await _sender.Send(new DeleteCategoryCommand(id));
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return NoContent();
        }
    }
}
