using Backend.Models;
using Backend.Services;
using Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        public CountryController(ICountryService countryService) {
            _countryService = countryService;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllCountry()
        {
            try {
                IEnumerable<CountryViewModelResponse> countries = await _countryService.GetAllCountryAsync();
                return Ok(new ResponseViewModel { Code = 0, Data = countries, Message="Get all country success!" }); 
            }
            catch(Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }
    }
}
