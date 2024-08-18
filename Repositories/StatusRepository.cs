using Backend.Models;

namespace Backend.Repositories
{
    public class StatusRepository : GenericRepository<Status>, IStatusRepository
    {
        public StatusRepository(JoyxphimContext dbContext) : base(dbContext)
        {

        }
    }
}
