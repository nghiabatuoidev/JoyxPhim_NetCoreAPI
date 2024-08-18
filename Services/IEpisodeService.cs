using Backend.Models;
using Backend.ViewModels;

namespace Backend.Services
{
    public interface IEpisodeService
    {
        public Task AddEpisodeAsync(int movieId, EpisodeViewModel episodeViewModel);
        public Task UpdateEpisodeAsync(int episodeId, EpisodeViewModel episodeViewModel);
        public Task<EpisodeViewModelResponse> GetEpisodeByIdAsync(int episode_id);
        public Task<IEnumerable<EpisodeViewModelResponse>> GetAllEpisodeAsync(int movieId);
        public Task DeleteEpisodeByIdAsync(int episodeId);
        public Task DeleteRangeEpisodeByIdAsync(List<int> episodeIds);
    }
}
