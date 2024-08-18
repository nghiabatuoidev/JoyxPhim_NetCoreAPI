using AutoMapper;
using Backend.Models;
using Backend.Repositories;
using Backend.Utils;
using Backend.ViewModels;
using Microsoft.IdentityModel.Tokens;


namespace Backend.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
      
        public MovieService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            
        }

        public async Task AddMovieAsync(MovieViewModel movieViewModel)
        {
            movieViewModel.Created = DateTime.Now;
            Movie movie = _mapper.Map<Movie>(movieViewModel);
            string movieNameSearch = StringUtils.RemoveDiacritics(movie.MovieName!);
            movie.MovieNameSearch = movieNameSearch;
            await _unitOfWork.MovieRepository!.AddAsync(movie);
            int count = await _unitOfWork.CompleteAsync();
            if (count < 0)
            {
                throw new Exception("Add movie failed!");
            }
            if (movieViewModel.Country_ids.Any())
            {
                foreach (int id in movieViewModel.Country_ids)
                {
                    MovieCountry movieCountry = new MovieCountry { CountryId = id, MovieId = movie.MovieId };
                    await _unitOfWork.MovieCountryRepository!.AddAsync(movieCountry);
                }
            }
            if (movieViewModel.Category_ids.Any())
            {
                foreach (int id in movieViewModel.Category_ids)
                {
                    MovieCategory movieCategory = new MovieCategory { CategoryId = id, MovieId = movie.MovieId };
                    await _unitOfWork.MovieCategoryRepository!.AddAsync(movieCategory);
                }
            }
            await _unitOfWork.CompleteAsync();
        }

        public async Task<int> DeleteMovieAsync(int id)
        {

            Movie movie = await _unitOfWork.MovieRepository!.GetByIdAsync(id);
            _unitOfWork.MovieRepository.Remove(movie);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteRangeMovieAsync(List<int> movieIds)
        {
            List<Movie> movies = new List<Movie>(); // Sử dụng List<Movie> để dễ dàng thêm phần tử
            foreach (int movieId in movieIds)
            {
                Movie movie = await _unitOfWork.MovieRepository!.GetByIdAsync(movieId);
                if (movie != null) // Kiểm tra null để tránh lỗi nếu movie không tồn tại
                {
                    movies.Add(movie); // Thêm vào danh sách
                }
            }

            // Gọi RemoveRange với danh sách movies
            _unitOfWork.MovieRepository!.RemoveRange(movies);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<int> UpdateMovieAsync(int movieId, MovieViewModel movieViewModel)
        {
            Movie movie = await _unitOfWork.MovieRepository!.GetByIdAsync(movieId);
            if (movie == null) { throw new Exception("Movie not found!"); }

            // Ánh xạ các thuộc tính từ movieViewModel vào thực thể movie hiện tại
            movieViewModel.Modified = DateTime.Now;
            _mapper.Map(movieViewModel, movie);
            _unitOfWork.MovieRepository!.Update(movie);

            // xóa các movie categroty cũ để add cái mới
            if (movieViewModel.Category_ids.Any())
            {
                IEnumerable<MovieCategory> movieCategories = await _unitOfWork.MovieCategoryRepository!.FindAsync(mc => mc.MovieId == movieId);
                if (movieCategories.Any())
                {
                    _unitOfWork.MovieCategoryRepository.RemoveRange(movieCategories);
                }
                foreach (int idx in movieViewModel.Category_ids)
                {
                    MovieCategory movieCategory = new MovieCategory { CategoryId = idx, MovieId = movieId };
                    await _unitOfWork.MovieCategoryRepository!.AddAsync(movieCategory);
                }
            }
            // xóa các movie country cũ để add cái mới
            if (movieViewModel.Country_ids.Any())
            {
                IEnumerable<MovieCountry> movieCountries = await _unitOfWork.MovieCountryRepository!.FindAsync(mc => mc.MovieId == movieId);
                if (movieCountries.Any())
                {
                    _unitOfWork.MovieCountryRepository.RemoveRange(movieCountries);
                }
                foreach (int idx in movieViewModel.Country_ids)
                {
                    MovieCountry movieCountry = new MovieCountry { CountryId = idx, MovieId = movieId };
                    await _unitOfWork.MovieCountryRepository!.AddAsync(movieCountry);
                }
            }
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<MovieViewModelResponse>> FindMovieByKeyword(string keyword)
        {
            IEnumerable<Movie> movies = await _unitOfWork.MovieRepository!.FindAsync(m =>m.MovieName!.Contains(keyword) || m.MovieOriginName!.Contains(keyword) || m.MovieNameSearch!.Contains(keyword) || m.DirectorName!.Contains(keyword) || m.ActorName!.Contains(keyword));
            //tạo list movie dto
            IEnumerable<MovieViewModelResponse> movieViewModels = await TransferAndIncludeDataAsync(movies);
            return movieViewModels;
        }

        public async Task<IEnumerable<MovieViewModelResponse>> GetAllMoviesAsync(int page, int pageSize)
        {
            if (pageSize < 1)
            {
                page = 1;
            }
            if (pageSize < 1) pageSize = 10;
            //lấy tất cả movie
            IEnumerable<Movie> movies = await _unitOfWork.MovieRepository!.GetAllAsync(filter: null, orderBy: m => m.OrderByDescending(movie => movie.MovieId), page, pageSize);
            //Data tranfers object
            IEnumerable<MovieViewModelResponse> moviesViewModel = await TransferAndIncludeDataAsync(movies);

            return moviesViewModel;
        }

        public async Task<MovieViewModelResponse> GetMovieByIdAsync(int id)
        {
            Movie movie = await _unitOfWork.MovieRepository!.GetByIdAsync(id);
            MovieViewModelResponse movieViewModel = _mapper.Map<MovieViewModelResponse>(movie);
            if (movie != null)
            {
                //include Status in Movie
                await _unitOfWork.MovieRepository!.IncludeSatusesAsync(movie);
                movieViewModel.Status = movie.Status?.Value?.Trim();
                //include YearRealeases in Movie
                await _unitOfWork.MovieRepository.IncludeYearReleasesAsync(movie);
                movieViewModel.Year = movie.YearRelease?.NumberYear;
                //include Type in Movie
                await _unitOfWork.MovieRepository.IncludeTypeAsync(movie);
                movieViewModel.TypeValue = movie.Type?.Value;
                //inclue Lang in Movie
                await _unitOfWork.MovieRepository.IncludeLangsAsync(movie);
                movieViewModel.LangValue = movie.Lang?.Value;
                //include Episode in Movie
                await _unitOfWork.MovieRepository.IncludeEpisodesAsync(movie);
                int max = 0;
                if (movie.Episodes.Any())
                {
                    //GET EPISODE CURRENT 
                    foreach (Episode episode in movie.Episodes)
                    {
                        if (episode.Name == null || episode.Name == "full" || episode.Name == "Full")
                        {
                            max = 0;
                        }
                        else
                        {
                            int value = Convert.ToInt32(episode.Name);
                            if (value > max)
                            {
                                max = value;
                            }
                        }
                    }
                    if (max == 0)
                    {
                        movieViewModel.EpisodeCurrent = "Full";
                    }
                    else
                    {
                        movieViewModel.EpisodeCurrent = max.ToString();
                    }
                }
                //lấy ra danh sách movie category thuộc movie
                IEnumerable<MovieCategory> movieCategories = await _unitOfWork.MovieCategoryRepository!.FindAsync(mc => mc.MovieId == movie.MovieId);
                if (movieCategories.Any())
                {
                    foreach (var movieCategory in movieCategories)
                    {
                        if (movieCategory != null)
                        {
                            //include Country trong MovieCategory
                            await _unitOfWork.MovieCategoryRepository.IncludeCategoryAsync(movieCategory);
                        }
                        if (!movieCategory!.Category!.CategoryName.IsNullOrEmpty() && movieCategory!.Category!.CategoryId > 0)
                        {
                            //thêm name category vào movie dto
                            movieViewModel.CategoriesName.Add(movieCategory.Category.CategoryName!);
                            movieViewModel.Category_ids.Add(movieCategory.Category.CategoryId);
                        }
                    }
                }
                //lấy ra danh sách MovieCountry thuộc Movie và thêm CountryName vào MovieDTO
                IEnumerable<MovieCountry> movieCountries = await _unitOfWork.MovieCountryRepository!.FindAsync(mc => mc.MovieId == movie.MovieId);
                if (movieCountries.Any())
                {
                    foreach (var movieCountry in movieCountries)
                    {
                        if (movieCountry != null)
                        {
                            //include Country trong MovieCategory
                            await _unitOfWork.MovieCountryRepository.IncludeCountryAsync(movieCountry);
                        }
                        if (!movieCountry!.Country!.CountryName.IsNullOrEmpty() && movieCountry!.Country!.CountryId > 0)
                        {
                            //thêm name category vào movie dto
                            movieViewModel.CountriesName.Add(movieCountry.Country.CountryName!);
                            movieViewModel.Country_ids.Add(movieCountry.Country.CountryId);
                        }
                    }
                }
            }
            return movieViewModel;
        }

        public async Task<IEnumerable<MovieViewModelResponse>> GetAllMoviesByCountryByIdAsync(int page, int pageSize, int countryId)
        {
            if (page < 1) page = 1;
            if (pageSize < 10) pageSize = 10;

            IEnumerable<Movie> movies = await _unitOfWork.MovieRepository!.GetAllAsync(
                filter: m => m.MovieCountries.Any(mc => mc.CountryId == countryId),
                orderBy: q => q.OrderByDescending(movie => movie.MovieId),
                page: page,
                pageSize: pageSize
            );

            IEnumerable<MovieViewModelResponse> movieViewModels = await TransferAndIncludeDataAsync(movies);

            return movieViewModels;
        }

        public async Task<IEnumerable<MovieViewModelResponse>> GetAllMoviesByCategoryByIdAsync(int page, int pageSize, int categoryId)
        {
            if (page < 1) page = 1;
            if (pageSize < 10) pageSize = 10;

            IEnumerable<Movie> movies = await _unitOfWork.MovieRepository!.GetAllAsync(
                 filter: m => m.MovieCategories.Any(mc => mc.CategoryId == categoryId),
                 orderBy: q => q.OrderByDescending(movie => movie.MovieId),
                 page: page,
                 pageSize: pageSize
             );
            IEnumerable<MovieViewModelResponse> movieViewModels = await TransferAndIncludeDataAsync(movies);


            return movieViewModels;
        }

        public async Task<IEnumerable<MovieViewModelResponse>> GetAllMoviesTheaterAsync(int page, int pageSize, bool isTheater)
        {
            if (page < 1) page = 1;
            if (pageSize < 10) pageSize = 10;

            IEnumerable<Movie> movies = await _unitOfWork.MovieRepository!.GetAllAsync(
                  filter: m => m.IsChieurap == isTheater,
                  orderBy: q => q.OrderByDescending(movie => movie.MovieId),
                  page: page,
                  pageSize: pageSize
              );
            IEnumerable<MovieViewModelResponse> movieViewModels = await TransferAndIncludeDataAsync(movies);

            return movieViewModels;
        }

        public async Task<IEnumerable<MovieViewModelResponse>> GetAllMoviesTrendingAsync(int page, int pageSize, bool isTrending)
        {
            if (page < 1) page = 1;
            if (pageSize < 10) pageSize = 10;

            IEnumerable<Movie> movies = await _unitOfWork.MovieRepository!.GetAllAsync(
                filter: m => m.IsTrending == isTrending,
                orderBy: q => q.OrderByDescending(movie => movie.MovieId),
                page: page,
                pageSize: pageSize
            );
            IEnumerable<MovieViewModelResponse> movieViewModels = await TransferAndIncludeDataAsync(movies);
            return movieViewModels;
        }

        public async Task<IEnumerable<MovieViewModelResponse>> GetAllMoviesByYearByIdAsync(int page, int pageSize, int yearId)
        {
            if (page < 1) page = 1;
            if (pageSize < 10) pageSize = 10;

            IEnumerable<Movie> movies = await _unitOfWork.MovieRepository!.GetAllAsync(
                filter: m => m.YearReleaseId == yearId,
                orderBy: q => q.OrderByDescending(movie => movie.MovieId),
                page: page,
                pageSize: pageSize
            );
            IEnumerable<MovieViewModelResponse> movieViewModels = await TransferAndIncludeDataAsync(movies);


            return movieViewModels;
        }

        public async Task<IEnumerable<MovieViewModelResponse>> SortAllMovieByCategoryIdAsync(int page, int pageSize, string type, int categoryId)
        {
            if (page < 1) page = 1;
            if (pageSize < 10) pageSize = 10;

            IEnumerable<MovieViewModelResponse>? movies = null;

            //if (type == "new")
            //{
            //    if (categoryId == 0)
            //    {
            //        movies = await GetAllMoviesAsync(page, pageSize);
            //    }
            //    else
            //    {
            //        movies = await GetAllMoviesByCategoryByIdAsync(page, pageSize, categoryId);
            //    }
            //}
            //if (type == "view")
            //{
            //    if (categoryId == 0)
            //    {
            //        movies = await _unitOfWork.MovieRepository!.GetAllAsync(
            //        orderBy: q => q.OrderByDescending(movie => movie.ViewNumber),
            //        page: page,
            //        pageSize: pageSize
            //        );
            //    }
            //    else
            //    {
            //        movies = await _unitOfWork.MovieRepository!.GetAllAsync(
            //        filter: m => m.MovieCategories.Any(mc => mc.CategoryId == categoryId),
            //        orderBy: q => q.OrderByDescending(movie => movie.ViewNumber),
            //        page: page,
            //        pageSize: pageSize
            //        );
            //    }
            //}
            //if (type == "rating")
            //{

            //}
            return movies;
        }
        public async Task<IEnumerable<MovieViewModelResponse>> TransferAndIncludeDataAsync(IEnumerable<Movie> movies)
        {
            List<MovieViewModelResponse> movieViewModels = new List<MovieViewModelResponse>();

            if (movies.Any())
            {
                foreach (Movie movie in movies)
                {
                    MovieViewModelResponse movieViewModel = _mapper.Map<MovieViewModelResponse>(movie);
                    //include Status in Movie
                    await _unitOfWork.MovieRepository!.IncludeSatusesAsync(movie);
                    movieViewModel.Status = movie.Status?.Value?.Trim();
                    //include YearRealeases in Movie
                    await _unitOfWork.MovieRepository.IncludeYearReleasesAsync(movie);
                    movieViewModel.Year = movie.YearRelease?.NumberYear;
                    //include Type in Movie
                    await _unitOfWork.MovieRepository.IncludeTypeAsync(movie);
                    movieViewModel.TypeValue = movie.Type?.Value;
                    //inclue Lang in Movie
                    await _unitOfWork.MovieRepository.IncludeLangsAsync(movie);
                    movieViewModel.LangValue = movie.Lang?.Value;
                    //include Episode in Movie
                    await _unitOfWork.MovieRepository.IncludeEpisodesAsync(movie);
                    int max = 0;
                    if (movie.Episodes.Any())
                    {
                        foreach (Episode episode in movie.Episodes)
                        {
                            if (episode.Name == null || episode.Name == "full" || episode.Name == "Full")
                            {
                                max = 0;
                            }
                            else
                            {
                                int value = Convert.ToInt32(episode.Name);
                                if (value > max)
                                {
                                    max = value;
                                }
                            }
                        }
                        if (max == 0)
                        {
                            movieViewModel.EpisodeCurrent = "Full";
                        }
                        else
                        {
                            movieViewModel.EpisodeCurrent = max.ToString();
                        }
                    }
                    //lấy ra danh sách movie category thuộc movie
                    IEnumerable<MovieCategory> movieCategories = await _unitOfWork.MovieCategoryRepository!.FindAsync(mc => mc.MovieId == movie.MovieId);
                    if (movieCategories.Any())
                    {
                        foreach (var movieCategory in movieCategories)
                        {
                            if (movieCategory != null)
                            {
                                //include Country trong MovieCategory
                                await _unitOfWork.MovieCategoryRepository.IncludeCategoryAsync(movieCategory);
                            }
                            if (!movieCategory!.Category!.CategoryName.IsNullOrEmpty() && movieCategory!.Category!.CategoryId > 0)
                            {
                                //thêm name category vào movie dto
                                movieViewModel.CategoriesName.Add(movieCategory.Category.CategoryName!);
                                movieViewModel.Category_ids.Add(movieCategory.Category.CategoryId);
                            }
                        }
                    }
                    //lấy ra danh sách MovieCountry thuộc Movie và thêm CountryName vào MovieDTO
                    IEnumerable<MovieCountry> movieCountries = await _unitOfWork.MovieCountryRepository!.FindAsync(mc => mc.MovieId == movie.MovieId);
                    if (movieCountries.Any())
                    {
                        foreach (var movieCountry in movieCountries)
                        {
                            if (movieCountry != null)
                            {
                                //include Country trong MovieCategory
                                await _unitOfWork.MovieCountryRepository.IncludeCountryAsync(movieCountry);
                            }
                            if (!movieCountry!.Country!.CountryName.IsNullOrEmpty() && movieCountry!.Country!.CountryId > 0)
                            {
                                //thêm name category vào movie dto
                                movieViewModel.CountriesName.Add(movieCountry.Country.CountryName!);
                                movieViewModel.Country_ids.Add(movieCountry.Country.CountryId);
                            }
                        }
                    }
                    movieViewModels.Add(movieViewModel);
                }
            }
            return movieViewModels;
        }

        public async Task<int> GetTotalItems()
        {
            int totalItems = await _unitOfWork.MovieRepository!.GetTotalCountAsync();
            return totalItems;
        }
        public int GetTotalPages(int totalItems, int pageSize)
        {
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            return totalPages;
        }

    }
}
