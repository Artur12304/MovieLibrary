namespace MovieLibrary.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MovieLibrary.Core.Interface.Services.Categories;
    using MovieLibrary.Core.Models.Categories.Commands;
    using MovieLibrary.Core.Models.Categories.Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [Route("/v1/CategoryManagement")]
    public class CategoryManagementController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryManagementController(ICategoryService movieService)
        {
            _categoryService = movieService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategories()
        {
            var movies = await _categoryService.GetAllCategoriesAsync();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
        {
            var movie = await _categoryService.GetCategoryByIdAsync(id);
            if (movie is null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CreateCategoryCommand command)
        {
            var createdId = await _categoryService.CreateCategoryAsync(command);
            return Ok(createdId);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<int>> UpdateCategory(UpdateCategoryCommand command)
        {
            var updatedId = await _categoryService.UpdateCategoryAsync(command);
            return Ok(updatedId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteCategory(int id)
        {
            var deletedId = await _categoryService.DeleteCategoryAsync(id);
            return Ok(deletedId);
        }
    }
}