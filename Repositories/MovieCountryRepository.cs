using Backend.Models;

namespace Backend.Repositories
{
    public class MovieCountryRepository : GenericRepository<MovieCountry>, IMovieCountryRepository
    {
        public MovieCountryRepository(JoyxphimContext dbContext) : base(dbContext)
        {
        }
    }
}
