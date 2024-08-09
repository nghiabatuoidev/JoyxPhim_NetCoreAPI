using Backend.Models;
using Backend.ViewModels;

namespace Backend.Repositories
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        public Task IncludeMovieCategoriesAsync(Movie movie);
        public Task IncludeMovieCountriesAsync(Movie movie);
        public Task IncludeMovieLangsAsync(Movie movie);
        public Task IncludeMovieEpisodesAsync(Movie movie);
        public Task IncludeMovieSatusesAsync(Movie movie);


    }
}
