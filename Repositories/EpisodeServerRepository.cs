using Backend.Models;
using Backend.Services;

namespace Backend.Repositories
{
    public class EpisodeServerRepository : GenericRepository<EpisodeServer>, IEpisodeServerRepository
    {
        public EpisodeServerRepository(JoyxphimContext dbContext) : base(dbContext) { 
        
        }
    }
}
