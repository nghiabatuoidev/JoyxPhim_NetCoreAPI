
using Backend.Services;
using Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;


namespace Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [AllowAnonymous]
        [HttpPost("add")]
        public async Task<IActionResult> AddMovie(MovieViewModel movieViewModel)
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

       
        [HttpPut("update/{movieId}")]
        public async Task<IActionResult> UpdateMovie(int movieId, [FromBody] MovieViewModel movieViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (movieId <= 0)
                {
                    return BadRequest();
                }
                await _movieService.UpdateMovieAsync(movieId, movieViewModel);
                return Ok(new ResponseViewModel { Code = 0, Message = $"Update movie {movieId} successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("find")]
        public async Task<IActionResult> FindMovieByKeyword([FromQuery] string keyword)
        {
            try
            {
                if(keyword == null)
                {
                    return NotFound();
                }
                IEnumerable<MovieViewModelResponse> movies = await _movieService.FindMovieByKeyword(keyword);
                if(movies.IsNullOrEmpty())
                {
                    return Ok(new ResponseViewModel { Code = 0, Data = null, Message = "Not Found!" });
                }
                return Ok(new ResponseViewModel { Code = 0, Data = movies, Message = "Find movie success!" });

            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }
       
        [HttpDelete("remove/{movie_id}")]
        public async Task<IActionResult> RemoveMovie(int movie_id)
        {
            try
            {
                if (movie_id <= 0)
                {
                    return BadRequest();
                }
                await _movieService.DeleteMovieAsync(movie_id);
                return Ok(new ResponseViewModel { Code = 0, Message = "Delete movie success!" });

            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }

       
        [HttpDelete("remove-range")]
        public async Task<IActionResult> RemoveRangeMovie([FromBody] List<int> movieIds)
        {
            try
            {
                if (movieIds.Count <= 0)
                {
                    return BadRequest();
                }
                await _movieService.DeleteRangeMovieAsync(movieIds);
                return Ok(new ResponseViewModel { Code = 0, Message = "Delete range movie success!" });

            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("{movieId}")]
        public async Task<IActionResult> GetMovieById(int movieId)
        {
            try
            {
                if (movieId <= 0)
                {
                    return BadRequest(new ResponseViewModel { Code = 2, Message = "Movie id is not valid!" });
                }
                MovieViewModelResponse movieViewModel = await _movieService.GetMovieByIdAsync(movieId);
                return Ok(new ResponseViewModel { Code = 0, Data = movieViewModel, Message = "Get movie successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpGet("list")]
        public async Task<IActionResult> GetAllMovies([FromQuery] int page = 1, int pageSize = 10)
        {
            try
            {
                IEnumerable<MovieViewModelResponse> movies = await _movieService.GetAllMoviesAsync(page, pageSize);
                int totalItems = await _movieService.GetTotalItems();
                int totalPages = _movieService.GetTotalPages(totalItems, pageSize);
                return Ok(new ResponseViewModel { Code = 0, Data = movies, totalItems = totalItems, totalPages = totalPages, Message = "Get movies successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = "Get movies failed!" });
            }
        }
        [AllowAnonymous]
        [HttpGet("list/country")]
        public async Task<IActionResult> GetAllMoviesByCountryId([FromQuery] int page = 1, int pageSize = 10, int countryId = 0)
        {
            try
            {
                if (countryId == 0 || countryId < 0)
                {
                    return BadRequest(new ResponseViewModel { Code = 2, Message = "Coutnry id is not valid!" });
                }
                IEnumerable<MovieViewModelResponse> movies = await _movieService.GetAllMoviesByCountryByIdAsync(page, pageSize, countryId);
                int totalItems = await _movieService.GetTotalItems();
                int totalPages = _movieService.GetTotalPages(totalItems, pageSize);
                return Ok(new ResponseViewModel { Code = 0, Data = movies, totalItems = totalItems, totalPages = totalPages, Message = "Get movies by country successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpGet("list/category")]
        public async Task<IActionResult> GetAllMoviesByCategoryId([FromQuery] int page = 1, int pageSize = 10, int categoryId = 0)
        {
            try
            {
                if (categoryId <= 0)
                {
                    return BadRequest(new ResponseViewModel { Code = 2, Message = "Category id is not valid!" });
                }
                IEnumerable<MovieViewModelResponse> movies = await _movieService.GetAllMoviesByCategoryByIdAsync(page, pageSize, categoryId);
                int totalItems = await _movieService.GetTotalItems();
                int totalPages = _movieService.GetTotalPages(totalItems, pageSize);
                return Ok(new ResponseViewModel { Code = 0, Data = movies, totalItems = totalItems, totalPages = totalPages, Message = "Get movies by category successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpGet("list/theater")]
        public async Task<IActionResult> GetAllMoviesTheater([FromQuery] int page = 1, int pageSize = 10, bool isTheater = true)
        {
            try
            {
                IEnumerable<MovieViewModelResponse> movies = await _movieService.GetAllMoviesTheaterAsync(page, pageSize, isTheater);
                int totalItems = await _movieService.GetTotalItems();
                int totalPages = _movieService.GetTotalPages(totalItems, pageSize);
                return Ok(new ResponseViewModel { Code = 0, Data = movies, totalItems = totalItems, totalPages = totalPages, Message = "Get movies theater successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpGet("list/trending")]
        public async Task<IActionResult> GetAllMoviesTrending([FromQuery] int page = 1, int pageSize = 10, bool isTrending = true)
        {
            try
            {
                IEnumerable<MovieViewModelResponse> movies = await _movieService.GetAllMoviesTheaterAsync(page, pageSize, isTrending);
                int totalItems = await _movieService.GetTotalItems();
                int totalPages = _movieService.GetTotalPages(totalItems, pageSize);
                return Ok(new ResponseViewModel { Code = 0, Data = movies, totalItems = totalItems, totalPages = totalPages, Message = "Get movies trending successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpGet("list/year")]
        public async Task<IActionResult> GetAllMoviesByYearId([FromQuery] int page = 1, int pageSize = 10, int yearId = 0)
        {
            try
            {
                if (yearId <= 0)
                {
                    return BadRequest(new ResponseViewModel { Code = 2, Message = "Category id is not valid!" });
                }
                IEnumerable<MovieViewModelResponse> movies = await _movieService.GetAllMoviesByYearByIdAsync(page, pageSize, yearId);

                int totalItems = await _movieService.GetTotalItems();
                int totalPages = _movieService.GetTotalPages(totalItems, pageSize);
                return Ok(new ResponseViewModel { Code = 0, Data = movies, totalItems = totalItems, totalPages = totalPages, Message = "Get movies by year successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpGet("list/sort")]
        public async Task<IActionResult> SortAllMoviesAsync([FromQuery] int page = 1, int pageSize = 10, string type = "new", int categoryId = 0)
        {
            try
            {
                if (categoryId <= 0)
                {
                    return BadRequest(new ResponseViewModel { Code = 2, Message = "Category id is not valid!" });
                }
                IEnumerable<MovieViewModelResponse> movies = await _movieService.SortAllMovieByCategoryIdAsync(page, pageSize, type, categoryId);

                int totalItems = await _movieService.GetTotalItems();
                int totalPages = _movieService.GetTotalPages(totalItems, pageSize);
                return Ok(new ResponseViewModel { Code = 0, Data = movies, totalItems = totalItems, totalPages = totalPages, Message = $"Sort movies by '{type}' successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }


    }
}
