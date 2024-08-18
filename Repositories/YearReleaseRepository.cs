using Backend.Models;

namespace Backend.Repositories
{
    public class YearReleaseRepository : GenericRepository<YearRelease>, IYearReleaseRepository
    {
        public YearReleaseRepository(JoyxphimContext dbContext) : base(dbContext) { }
    }
}
