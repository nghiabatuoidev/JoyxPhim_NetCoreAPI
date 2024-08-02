
using Backend.Services;
using Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;

using Backend.Models;


namespace Backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddMovie([FromForm] MovieViewModel movieViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _movieService.AddMovieAsync(movieViewModel);
                return Ok(new ResponseViewModel { Code = 0, Message = "Add movie successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }

        [HttpGet("find")]
        public async Task<IActionResult> FindMovieByKeyword(string keyword)
        {
            try
            {
                IEnumerable<Movie> movies = await _movieService.FindMovieByKeyword(keyword);
                return Ok(new ResponseViewModel { Code = 0, Data = movies, Message =  "Find movie success!"});

            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int? id)
        {
            try
            {
                if (id == null || id < 0)
                {
                    return BadRequest(new ResponseViewModel { Code = 2, Message = "Movie id is not valid!" });
                }
                Movie movie = await _movieService.GetMovieByIdAsync(id.Value);
                return Ok(new ResponseViewModel { Code = 0, Data = movie, Message = "Get movie successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllMovies([FromQuery] int page = 1, int pageSize = 10)
        {
            try
            {
                IEnumerable<Movie> movies = await _movieService.GetAllMoviesAsync(page, pageSize);
                return Ok(new ResponseViewModel { Code = 0, Data = movies, Message = "Get movies successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = "Get movies failed!" });
            }
        }

        [HttpGet("list/country")]
        public async Task<IActionResult> GetAllMoviesByCountryId([FromQuery] int page = 1, int pageSize = 10, int countryId = 0)
        {
            try
            {
                if (countryId == 0 || countryId < 0)
                {
                    return BadRequest(new ResponseViewModel { Code = 2, Message = "Coutnry id is not valid!" });
                }
                IEnumerable<Movie> movies = await _movieService.GetAllMoviesByCountryByIdAsync(page, pageSize, countryId);
                return Ok(new ResponseViewModel { Code = 0, Data = movies, Message = "Get movies by country successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }

        [HttpGet("list/category")]
        public async Task<IActionResult> GetAllMoviesByCategoryId([FromQuery] int page = 1, int pageSize = 10, int categoryId = 0)
        {
            try
            {
                if (categoryId == 0 || categoryId < 0)
                {
                    return BadRequest(new ResponseViewModel { Code = 2, Message = "Category id is not valid!" });
                }
                IEnumerable<Movie> movies = await _movieService.GetAllMoviesByCategoryByIdAsync(page, pageSize, categoryId);
                return Ok(new ResponseViewModel { Code = 0, Data = movies, Message = "Get movies by category successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }

        [HttpGet("list/theater")]
        public async Task<IActionResult> GetAllMoviesTheater([FromQuery] int page = 1, int pageSize = 10, bool isTheater = true)
        {
            try
            {
                IEnumerable<Movie> movies = await _movieService.GetAllMoviesTheaterAsync(page, pageSize, isTheater);
                return Ok(new ResponseViewModel { Code = 0, Data = movies, Message = "Get movies theater successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }

        [HttpGet("list/trending")]
        public async Task<IActionResult> GetAllMoviesTrending([FromQuery] int page = 1, int pageSize = 10, bool isTrending = true)
        {
            try
            {
                IEnumerable<Movie> movies = await _movieService.GetAllMoviesTheaterAsync(page, pageSize, isTrending);
                return Ok(new ResponseViewModel { Code = 0, Data = movies, Message = "Get movies trending successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }

        [HttpGet("list/year")]
        public async Task<IActionResult> GetAllMoviesByYearId([FromQuery] int page = 1, int pageSize = 10, int yearId = 0)
        {
            try
            {
                if (yearId == 0 || yearId < 0)
                {
                    return BadRequest(new ResponseViewModel { Code = 2, Message = "Category id is not valid!" });
                }
                IEnumerable<Movie> movies = await _movieService.GetAllMoviesByYearByIdAsync(page, pageSize, yearId);
                return Ok(new ResponseViewModel { Code = 0, Data = movies, Message = "Get movies by year successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }

        [HttpGet("list/sort")]
        public async Task<IActionResult> SortAllMoviesAsync([FromQuery] int page = 1, int pageSize = 10, string type = "new", int categoryId = 0)
        {
            try
            {
                if (categoryId < 0)
                {
                    return BadRequest(new ResponseViewModel { Code = 2, Message = "Category id is not valid!" });
                }
                IEnumerable<Movie> movies = await _movieService.SortAllMovieByCategoryIdAsync(page, pageSize, type, categoryId);
                return Ok(new ResponseViewModel { Code = 0, Data = movies, Message = $"Sort movies by '{type}' successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }


    }
}
