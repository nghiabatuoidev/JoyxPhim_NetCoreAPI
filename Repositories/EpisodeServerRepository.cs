using Backend.Models;
using Backend.Services;

namespace Backend.Repositories
{
    public class EpisodeServerRepository : GenericRepository<EpisodeServer>, IEpisodeServerRepository
    {
        public EpisodeServerRepository(JoyxphimContext dbContext) : base(dbContext) { 
        
        }
        public async Task IncludeSeverAsync(EpisodeServer episodeServer)
        {
            await _dbContext.Entry(episodeServer)
                    .Reference(mc => mc.Server)
                    .LoadAsync();
        }
    }
}
