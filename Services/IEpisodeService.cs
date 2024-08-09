using Backend.Models;
using Backend.ViewModels;

namespace Backend.Services
{
    public interface IEpisodeService
    {
        public Task AddEpisodeAsync(EpisodeViewModel episodeViewModel);
        public Task UpdateEpisodeAsync(int episode_id, EpisodeViewModel episodeViewModel);
        public Task<Episode> GetEpisodeByIdAsync(int episode_id);
        public Task<IEnumerable<Episode>> GetAllEpisodeAsync(int page, int pageSize);
        public Task DeleteEpisodeByIdAsync(int episode_id);
    }
}
