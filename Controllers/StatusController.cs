using Backend.Models;
using Backend.Services;
using Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;
        public StatusController(IStatusService statusService) {
            _statusService = statusService;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllStatus()
        {
            try {
                IEnumerable<StatusViewModelResponse> statuses = await _statusService.GetAllStatusAsync();
                return Ok(new ResponseViewModel { Code = 0, Data = statuses, Message="Get all status success!" }); 
            }
            catch(Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }
    }
}
