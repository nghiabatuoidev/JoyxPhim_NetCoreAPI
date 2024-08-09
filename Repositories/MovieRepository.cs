using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        public MovieRepository(JoyxphimContext dbContext) : base(dbContext) { }
        public async Task IncludeMovieCountriesAsync(Movie movie)
        {
            // Sử dụng Entry để nạp MovieCategories nếu movie đã được lấy
            await _dbContext.Entry(movie)
                .Collection(m => m.MovieCountries)
                .LoadAsync();
        }
        public async Task IncludeMovieCategoriesAsync(Movie movie)
        {
            // Sử dụng Entry để nạp MovieCategories nếu movie đã được lấy
            await _dbContext.Entry(movie)
                .Collection(m => m.MovieCategories)
                .LoadAsync();
        }

        public async Task IncludeMovieLangsAsync(Movie movie)
        {

            await _dbContext.Entry(movie)
                .Reference(m => m.Lang)
                .LoadAsync();
        }

        public async Task IncludeMovieEpisodesAsync(Movie movie)
        {
            // Sử dụng Entry để nạp MovieCategories nếu movie đã được lấy
            await _dbContext.Entry(movie)
                .Collection(m => m.Episodes)
                .LoadAsync();
        }

        public async Task IncludeMovieSatusesAsync(Movie movie)
        {
            // Sử dụng Entry để nạp MovieCategories nếu movie đã được lấy
            await _dbContext.Entry(movie)
                .Reference(m => m.Status)
                .LoadAsync();
        }
    }
}
