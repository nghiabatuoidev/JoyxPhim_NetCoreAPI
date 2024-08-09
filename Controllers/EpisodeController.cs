
using Backend.Models;
using Backend.Services;
using Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EpisodeController : ControllerBase
    {
        private readonly IEpisodeService _episodeService;
        public EpisodeController(IEpisodeService episodeService)
        {
            _episodeService = episodeService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddEpisode([FromForm] EpisodeViewModel episodeViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _episodeService.AddEpisodeAsync(episodeViewModel);
                return Ok(new ResponseViewModel { Code = 0, Message = "Add episode successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }
        [HttpPut("update/{episode_id}")]
        public async Task<IActionResult> UpdateEpisode(int episode_id, [FromForm] EpisodeViewModel episodeViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (episode_id == null || episode_id <= 0)
                {
                    return BadRequest(episode_id);
                }
                await _episodeService.UpdateEpisodeAsync(episode_id, episodeViewModel);
                return Ok(new ResponseViewModel { Code = 0, Message = $"Update episode {episode_id} successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }

        [HttpGet("{episode_id}")]
        public async Task<IActionResult> GetEpisodeById(int episode_id)
        {
            try
            {
                if (episode_id <= 0 || episode_id == null)
                {
                    return BadRequest(episode_id);
                }
                Episode episode = await _episodeService.GetEpisodeByIdAsync(episode_id);
                if (episode == null)
                {
                    return NotFound(new ResponseViewModel { Code = 2, Message = "Episode Not Found" });
                }
                return Ok(new ResponseViewModel { Code = 0, Data = episode, Message = $"Get episode {episode_id} successfully!"});
            }
            catch (Exception ex) {
                return BadRequest(new ResponseViewModel { Code = 1, Data = ex.Message});
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllEpisode([FromQuery]int page, [FromQuery]int pageSize)
        {
            try
            {
                IEnumerable<Episode> episodes = await _episodeService.GetAllEpisodeAsync(page,pageSize);
                if (episodes == null)
                {
                    return NotFound(new ResponseViewModel { Code = 2, Message = "Episodes Not Found" });
                }
                return Ok(new ResponseViewModel { Code = 0, Data = episodes, Message = "Get all episodes successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Data = ex.Message });
            }
        }

        [HttpDelete("remove/{episode_id}")]
        public async Task<IActionResult> RemoveEpisode(int episode_id)
        {
            try
            {
                if (episode_id <= 0 || episode_id == null)
                {
                    return BadRequest(episode_id);
                }
                await _episodeService.DeleteEpisodeByIdAsync(episode_id);
               
                return Ok(new ResponseViewModel { Code = 0, Message = $"Remove episode {episode_id} successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Data = ex.Message });
            }
        }
    }
}
