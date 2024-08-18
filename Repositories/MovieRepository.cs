using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        public MovieRepository(JoyxphimContext dbContext) : base(dbContext) { }
        public async Task IncludeMovieCountriesAsync(Movie movie)
        {
            await _dbContext.Entry(movie)
                    .Collection(m => m.MovieCountries)
                    .LoadAsync();
        }
        public async Task IncludeMovieCategoriesAsync(Movie movie)
        {
            // Nạp MovieCountries nếu chưa được nạp
            if (!_dbContext.Entry(movie).Collection(m => m.MovieCategories).IsLoaded)
            {
                await _dbContext.Entry(movie)
                    .Collection(m => m.MovieCategories)
                    .LoadAsync();
            }
        }

        public async Task IncludeLangsAsync(Movie movie)
        {

            await _dbContext.Entry(movie)
                .Reference(m => m.Lang)
                .LoadAsync();
        }

        public async Task IncludeEpisodesAsync(Movie movie)
        {
            await _dbContext.Entry(movie)
                .Collection(m => m.Episodes)
                .LoadAsync();
        }

        public async Task IncludeSatusesAsync(Movie movie)
        {
            await _dbContext.Entry(movie)
                .Reference(m => m.Status)
                .LoadAsync();
        }

        public async Task IncludeYearReleasesAsync(Movie movie)
        {
            await _dbContext.Entry(movie)
                .Reference(m => m.YearRelease)
                .LoadAsync();
        }

        public async Task IncludeTypeAsync(Movie movie)
        {
            await _dbContext.Entry(movie)
                .Reference(m => m.Type)
                .LoadAsync();
        }
    }
}
