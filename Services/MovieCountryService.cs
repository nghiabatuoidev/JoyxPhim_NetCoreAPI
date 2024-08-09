using Backend.Models;
using Backend.Repositories;
using Backend.ViewModels;

namespace Backend.Services
{
    public class MovieCountryService : IMovieCountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MovieCountryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddMovieCountryAsync(int movie_id, int country_id)
        {
            MovieCountry movieCountry = new MovieCountry { MovieId = movie_id, CountryId = country_id};
            await _unitOfWork.MovieCountryRepository!.AddAsync(movieCountry);
            int count = await _unitOfWork.CompleteAsync();
            if (count <= 0) {
                throw new Exception("Add country to movie fail!");
            }
        }

        public async Task DeleteMovieCountryAsync(int movieCountry_id)
        {
            MovieCountry movieCountry = await _unitOfWork.MovieCountryRepository!.GetByIdAsync(movieCountry_id);
            if(movieCountry != null)
            {
                 _unitOfWork.MovieCountryRepository.Remove(movieCountry);
                await _unitOfWork.CompleteAsync();
            }
            throw new Exception("Not found!");
        }
    }
}
