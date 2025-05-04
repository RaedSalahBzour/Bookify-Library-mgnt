using Bookify_Library_mgnt.Dtos.Categories;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookify_Library_mgnt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories(int pageNumber = 1, int pageSize = 10)
        {
            var categories = await _categoryRepository.GetCategoriesAsync(pageNumber, pageSize);
            return Ok(categories);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] string id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return BadRequest("not found");
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto categoryDto)
        {
            if (categoryDto is null)
            {
                return BadRequest("u must provide a category information");
            }
            var category = await _categoryRepository.CreateCategoryAsync(categoryDto);
            return CreatedAtAction(nameof(GetCategoryById), new { Id = category.Id }, categoryDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] string id, [FromBody] UpdateCategoryDto categoryDto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return null;
            }
            await _categoryRepository.UpdateCategoryAsync(id, categoryDto);
            return Ok(categoryDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] string id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return BadRequest("not found");
            }
            await _categoryRepository.DeleteCategoryAsync(id);
            return NoContent();

        }
    }
}
