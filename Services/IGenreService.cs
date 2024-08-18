using Backend.Models;
using Backend.ViewModels;

namespace Backend.Services
{
    public interface IGenreService
    {
        public Task<IEnumerable<GenreViewModelResponse>> GetAllGenreAsync();
    }
}
