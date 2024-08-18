using Backend.Services;
using Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LangController : ControllerBase
    {
        private readonly ILangService _langService;
        public LangController(ILangService langService)
        {
            _langService = langService;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllLang()
        {
            try
            {
                IEnumerable<LangViewModelReponse> langs = await _langService.GetAllLangAsync();
                return Ok(new ResponseViewModel { Code = 0, Data = langs, Message = "Get all Lang success!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }
    }
}
