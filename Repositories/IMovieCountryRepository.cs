using Backend.Models;

namespace Backend.Repositories
{
    public interface IMovieCountryRepository : IGenericRepository<MovieCountry>
    {
        public Task IncludeCountryAsync(MovieCountry movieCountry);
    }
}
