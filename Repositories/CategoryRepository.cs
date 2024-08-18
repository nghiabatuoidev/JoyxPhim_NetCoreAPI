using Backend.Models;

namespace Backend.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(JoyxphimContext dbContext) : base(dbContext)
        {

        }
    }
}
