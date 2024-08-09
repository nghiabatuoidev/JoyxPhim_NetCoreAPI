
using Backend.Models;
using Backend.Repositories;

namespace Backend.Services
{
    public class MovieCategoryService : IMovieCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MovieCategoryService(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        public async Task AddMovieCategory(int movie_id, int category_id)
        {
            MovieCategory movieCategory = new MovieCategory { MovieId = movie_id, CategoryId = category_id };
            await _unitOfWork.MovieCategoryRepository!.AddAsync(movieCategory);
            int count = await _unitOfWork.CompleteAsync();
            if (count <= 0)
            {
                throw new Exception("Add category to movie fail!");
            }
        }

        public Task RemoveMovieCategory(int movieCategory_id)
        {
            throw new NotImplementedException();
        }
    }
}
