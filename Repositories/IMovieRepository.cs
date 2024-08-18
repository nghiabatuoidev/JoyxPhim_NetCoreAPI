using Backend.Models;
using Backend.ViewModels;

namespace Backend.Repositories
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        public Task IncludeMovieCategoriesAsync(Movie movie);
        public Task IncludeMovieCountriesAsync(Movie movie);
        public Task IncludeLangsAsync(Movie movie);
        public Task IncludeEpisodesAsync(Movie movie);
        public Task IncludeSatusesAsync(Movie movie);
        public Task IncludeYearReleasesAsync(Movie movie);
        public Task IncludeTypeAsync(Movie movie);

    }
}
