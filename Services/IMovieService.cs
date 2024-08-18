using Backend.Models;
using Backend.ViewModels;

namespace Backend.Services
{
    public interface IMovieService
    {
        // Thêm một bộ phim mới
        Task AddMovieAsync(MovieViewModel movieViewModel);

        // Sửa thông tin một bộ phim
        Task<int> UpdateMovieAsync(int movieId, MovieViewModel movieViewModel);

        // Xóa một bộ phim
        Task<int> DeleteMovieAsync(int id);
        Task DeleteRangeMovieAsync(List<int> movieIds);

        // Lấy thông tin của một bộ phim theo id
        Task<MovieViewModelResponse> GetMovieByIdAsync(int id);

        // Lấy danh sách tất cả các bộ phim
        Task<IEnumerable<MovieViewModelResponse>> GetAllMoviesAsync(int page, int pageSize);
        Task<IEnumerable<MovieViewModelResponse>> GetAllMoviesByCategoryByIdAsync(int page, int pageSize, int categoryId);
        Task<IEnumerable<MovieViewModelResponse>> GetAllMoviesByCountryByIdAsync(int page, int pageSize, int countryId);
        Task<IEnumerable<MovieViewModelResponse>> GetAllMoviesTrendingAsync(int page, int pageSize, bool isTrending);
        Task<IEnumerable<MovieViewModelResponse>> GetAllMoviesTheaterAsync(int page, int pageSize, bool isTheater);
        Task<IEnumerable<MovieViewModelResponse>> GetAllMoviesByYearByIdAsync(int page, int pageSize, int yearId);
        Task<IEnumerable<MovieViewModelResponse>> SortAllMovieByCategoryIdAsync(int page, int pageSize, string type, int categoryId);
        //
        Task<IEnumerable<MovieViewModelResponse>> FindMovieByKeyword(string keyword);
        //
        Task<IEnumerable<MovieViewModelResponse>> TransferAndIncludeDataAsync(IEnumerable<Movie> movies);
        //
        Task<int> GetTotalItems();
        int GetTotalPages(int totalItems, int pageSize);

    }
}
