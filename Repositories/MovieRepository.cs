using Backend.Models;

namespace Backend.Repositories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository 
    {
        public MovieRepository(JoyxphimContext dbContext) : base(dbContext) { }
    }
}
