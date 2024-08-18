using Backend.Models;
using Backend.Services;

namespace Backend.Repositories
{
    public interface IEpisodeServerRepository : IGenericRepository<EpisodeServer>
    {
        public Task IncludeSeverAsync(EpisodeServer episodeServer);
    }
}
