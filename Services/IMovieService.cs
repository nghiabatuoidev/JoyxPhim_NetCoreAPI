using Backend.Models;
using Backend.ViewModels;

namespace Backend.Services
{
    public interface IMovieService
    {
        // Thêm một bộ phim mới
        Task AddMovieAsync(MovieViewModel movieViewModel);

        // Sửa thông tin một bộ phim
        Task<int> UpdateMovieAsync(int movieId,MovieViewModel movieViewModel);

        // Xóa một bộ phim
        Task<int> DeleteMovieAsync(int id);

        // Lấy thông tin của một bộ phim theo id
        Task<Movie> GetMovieByIdAsync(int id);

        // Lấy danh sách tất cả các bộ phim
        Task<IEnumerable<Movie>> GetAllMoviesAsync(int page, int pageSize);
        Task<IEnumerable<Movie>> GetAllMoviesByCategoryByIdAsync(int page, int pageSize, int categoryId);
        Task<IEnumerable<Movie>> GetAllMoviesByCountryByIdAsync(int page, int pageSize, int countryId);
        Task<IEnumerable<Movie>> GetAllMoviesTrendingAsync(int page, int pageSize, bool isTrending);
        Task<IEnumerable<Movie>> GetAllMoviesTheaterAsync(int page, int pageSize, bool isTheater);
        Task<IEnumerable<Movie>> GetAllMoviesByYearByIdAsync(int page, int pageSize, int yearId);
        Task<IEnumerable<Movie>> SortAllMovieByCategoryIdAsync(int page, int pageSize, string type, int categoryId);

        //
        Task<IEnumerable<Movie>> FindMovieByKeyword(string keyword);
    }
}
