using Backend.Models;

namespace Backend.Repositories
{
    public class MovieCategoryRepository : GenericRepository<MovieCategory>, IMovieCategoryRepository 
    {
        public MovieCategoryRepository(JoyxphimContext dbContext) : base(dbContext) {
        
        }
    }
}
