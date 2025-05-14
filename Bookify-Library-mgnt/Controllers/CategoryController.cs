using Bookify_Library_mgnt.Dtos.Categories;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Bookify_Library_mgnt.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookify_Library_mgnt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories(int pageNumber = 1, int pageSize = 10)
        {
            var categories = await _categoryService.GetCategoriesAsync(pageNumber, pageSize);
            return Ok(categories);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] string id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto categoryDto)
        {
            var result = await _categoryService.CreateCategoryAsync(categoryDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return CreatedAtAction(nameof(GetCategoryById), new { Id = result.Data.Id }, categoryDto);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory([FromRoute] string id, [FromBody] UpdateCategoryDto categoryDto)
        {
            var result = await _categoryService.UpdateCategoryAsync(id, categoryDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return Ok(categoryDto);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory([FromRoute] string id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return NoContent();
        }
    }
}
