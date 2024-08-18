using Backend.Models;
using Backend.Services;
using Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class YearReleaseController : ControllerBase
    {
        private readonly IYearReleaseService _yearReleaseService;
        public YearReleaseController(IYearReleaseService yearReleaseService) {
            _yearReleaseService = yearReleaseService;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllYearRelease()
        {
            try {
                IEnumerable<YearViewModelResponse> yearReleases = await _yearReleaseService.GetAllYearReleasesAsync();
                return Ok(new ResponseViewModel { Code = 0, Data = yearReleases, Message="Get all year release success!" }); 
            }
            catch(Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }
    }
}
