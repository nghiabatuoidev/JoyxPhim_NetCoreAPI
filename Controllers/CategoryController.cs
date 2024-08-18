using Backend.Models;
using Backend.Services;
using Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService) {
            _categoryService = categoryService;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllCategory()
        {
            try {
                IEnumerable<CategoryViewModelResponse> categories = await _categoryService.GetAllCategoryAsync();
                return Ok(new ResponseViewModel { Code = 0, Data = categories, Message="Get all categories success!" }); 
            }
            catch(Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }
    }
}
