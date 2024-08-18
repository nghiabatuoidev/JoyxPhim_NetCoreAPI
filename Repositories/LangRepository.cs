using Backend.Models;

namespace Backend.Repositories
{
    public class LangRepository : GenericRepository<Lang>, ILangRepository
    {
        public LangRepository(JoyxphimContext dbContext) : base(dbContext) { }
    }
}
