using Backend.Models;
using Backend.Services;
using Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService) {
            _genreService = genreService;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllGenre()
        {
            try {
                IEnumerable<GenreViewModelResponse> genres = await _genreService.GetAllGenreAsync();
                return Ok(new ResponseViewModel { Code = 0, Data = genres, Message="Get all genre success!" }); 
            }
            catch(Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }
    }
}
