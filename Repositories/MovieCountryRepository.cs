using Backend.Models;

namespace Backend.Repositories
{
    public class MovieCountryRepository : GenericRepository<MovieCountry>, IMovieCountryRepository
    {
        public MovieCountryRepository(JoyxphimContext dbContext) : base(dbContext)
        {
        }
        public async Task IncludeCountryAsync(MovieCountry movieCountry)
        {
            await _dbContext.Entry(movieCountry)
                    .Reference(mc => mc.Country)
                    .LoadAsync();
        }
    }
}
