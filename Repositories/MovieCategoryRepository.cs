using Backend.Models;

namespace Backend.Repositories
{
    public class MovieCategoryRepository : GenericRepository<MovieCategory>, IMovieCategoryRepository 
    {
        public MovieCategoryRepository(JoyxphimContext dbContext) : base(dbContext) {
        
        }
        public async Task IncludeCategoryAsync(MovieCategory movieCategory)
        {
            await _dbContext.Entry(movieCategory)
                    .Reference(mc => mc.Category)
                    .LoadAsync();
        }
    }
}
