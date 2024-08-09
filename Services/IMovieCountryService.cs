using Backend.ViewModels;

namespace Backend.Services
{
    public interface IMovieCountryService
    {
        public Task AddMovieCountryAsync(int movie_id, int country_id);
        public Task DeleteMovieCountryAsync(int movieCountry_Id);
    }
}
