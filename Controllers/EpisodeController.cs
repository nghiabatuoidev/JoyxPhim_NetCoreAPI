using Backend.Services;
using Backend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EpisodeController : ControllerBase
    {
        private readonly IEpisodeService _episodeService;
        public EpisodeController(IEpisodeService episodeService)
        {
            _episodeService = episodeService;
        }

        [HttpPost("add/{movieId}")]
        public async Task<IActionResult> AddEpisode(int movieId, [FromBody] EpisodeViewModel episodeViewModel)
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
                await _episodeService.AddEpisodeAsync(movieId, episodeViewModel);
                return Ok(new ResponseViewModel { Code = 0, Message = "Add episode successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }

     
        [HttpPut("update/{episode_id}")]
        public async Task<IActionResult> UpdateEpisode(int episode_id, [FromBody] EpisodeViewModel episodeViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (episode_id <= 0)
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

        [AllowAnonymous]
        [HttpGet("{episode_id}")]
        public async Task<IActionResult> GetEpisodeById(int episode_id)
        {
            try
            {
                if (episode_id <= 0 )
                {
                    return BadRequest(episode_id);
                }
                EpisodeViewModelResponse episode = await _episodeService.GetEpisodeByIdAsync(episode_id);
                if (episode == null)
                {
                    return NotFound(new ResponseViewModel { Code = 1, Message = "Episode Not Found" });
                }
                return Ok(new ResponseViewModel { Code = 0, Data = episode, Message = $"Get episode {episode_id} successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Data = ex.Message });
            }
        }
        [AllowAnonymous]

        [HttpGet("get-all/{movieId}")]
        public async Task<IActionResult> GetAllEpisode(int movieId)
        {
            try
            {
                IEnumerable<EpisodeViewModelResponse> episodes = await _episodeService.GetAllEpisodeAsync(movieId);
                if (episodes == null)
                {
                    return NotFound(new ResponseViewModel { Code = 1, Message = "Episodes Not Found" });
                }
                return Ok(new ResponseViewModel { Code = 0, Data = episodes, Message = "Get all episodes successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Data = ex.Message });
            }
        }
        [Authorize]
        [HttpDelete("remove/{episode_id}")]
        public async Task<IActionResult> RemoveEpisode(int episode_id)
        {
            try
            {
                if (episode_id <= 0)
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

     
        [HttpDelete("remove-range")]
        public async Task<IActionResult> RemoveRangeEpisode([FromBody] List<int> episodeIds)
        {
            try
            {
                if (episodeIds.Count <= 0)
                {
                    return NotFound();
                }
                await _episodeService.DeleteRangeEpisodeByIdAsync(episodeIds);

                return Ok(new ResponseViewModel { Code = 0, Message = $"Remove range episode successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Data = ex.Message });
            }
        }
    }
}
