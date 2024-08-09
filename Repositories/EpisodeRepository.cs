using Backend.Models;

namespace Backend.Repositories
{
    public class EpisodeRepository : GenericRepository<Episode>, IEpisodeRepository
    {
        public EpisodeRepository(JoyxphimContext dbContext) : base(dbContext) { }
    }
}
