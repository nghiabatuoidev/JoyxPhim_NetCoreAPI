using AutoMapper;
using Backend.Models;
using Backend.Repositories;
using Backend.ViewModels;


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
            await _unitOfWork.MovieRepository!.AddAsync(movie);
            int count = await _unitOfWork.CompleteAsync();
            if (count < 0)
            {
                throw new Exception("Add movie failed!");
            }
        }

        public async Task<int> DeleteMovieAsync(int id)
        {

            Movie movie = await _unitOfWork.MovieRepository!.GetByIdAsync(id);
            _unitOfWork.MovieRepository.Remove(movie);
            return await _unitOfWork.CompleteAsync();
        }


        public async Task<int> UpdateMovieAsync(int id, MovieViewModel movieViewModel)
        {
            Movie movie = _mapper.Map<Movie>(movieViewModel);
            _unitOfWork.MovieRepository.Update(movie);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<Movie>> FindMovieByKeyword(string keyword)
        {
            IEnumerable<Movie> movies = await _unitOfWork.MovieRepository!.FindAsync(m => m.Slug.Contains(keyword) || m.MovieOriginName.Contains(keyword) || m.DirectorName.Contains(keyword) || m.ActorName.Contains(keyword));
            if (movies.Any())
            {

                return movies;
            }
            throw new Exception("Not found!");
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync(int page, int pageSize)
        {
            if (pageSize < 1)
            {
                page = 1;
            }
            if (pageSize < 1) pageSize = 10;

            IEnumerable<Movie> movies = await _unitOfWork.MovieRepository!.GetAllAsync(filter: null, orderBy: m => m.OrderByDescending(movie => movie.MovieId), page, pageSize);
            return movies;
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {

            Movie movie = await _unitOfWork.MovieRepository!.GetByIdAsync(id);
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesByCountryByIdAsync(int page, int pageSize, int countryId)
        {
            if (page < 1) page = 1;
            if (pageSize < 10) pageSize = 10;

            IEnumerable<Movie> movies = await _unitOfWork.MovieRepository!.GetAllAsync(
                filter: m => m.MovieCountries.Any(mc => mc.CountryId == countryId),
                orderBy: q => q.OrderByDescending(movie => movie.MovieId),
                page: page,
                pageSize: pageSize
            );
            return movies;
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesByCategoryByIdAsync(int page, int pageSize, int categoryId)
        {
            if (page < 1) page = 1;
            if (pageSize < 10) pageSize = 10;

            IEnumerable<Movie> movies = await _unitOfWork.MovieRepository!.GetAllAsync(
                 filter: m => m.MovieCategories.Any(mc => mc.CategoryId == categoryId),
                 orderBy: q => q.OrderByDescending(movie => movie.MovieId),
                 page: page,
                 pageSize: pageSize
             );
            return movies;
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesTheaterAsync(int page, int pageSize, bool isTheater)
        {
            if (page < 1) page = 1;
            if (pageSize < 10) pageSize = 10;

            IEnumerable<Movie> movies = await _unitOfWork.MovieRepository!.GetAllAsync(
                  filter: m => m.IsChieurap == isTheater,
                  orderBy: q => q.OrderByDescending(movie => movie.MovieId),
                  page: page,
                  pageSize: pageSize
              );
            return movies;
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesTrendingAsync(int page, int pageSize, bool isTrending)
        {
            if (page < 1) page = 1;
            if (pageSize < 10) pageSize = 10;

            IEnumerable<Movie> movies = await _unitOfWork.MovieRepository!.GetAllAsync(
                filter: m => m.IsTrending == isTrending,
                orderBy: q => q.OrderByDescending(movie => movie.MovieId),
                page: page,
                pageSize: pageSize
            );
            return movies;
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesByYearByIdAsync(int page, int pageSize, int yearId)
        {
            if (page < 1) page = 1;
            if (pageSize < 10) pageSize = 10;

            IEnumerable<Movie> movies = await _unitOfWork.MovieRepository!.GetAllAsync(
                filter: m => m.YearReleaseId == yearId,
                orderBy: q => q.OrderByDescending(movie => movie.MovieId),
                page: page,
                pageSize: pageSize
            );
            return movies;
        }

        public async Task<IEnumerable<Movie>> SortAllMovieByCategoryIdAsync(int page, int pageSize, string type, int categoryId)
        {
            if (page < 1) page = 1;
            if (pageSize < 10) pageSize = 10;
            IEnumerable<Movie>? movies = null;

            if (type == "new")
            {
                if (categoryId == 0)
                {
                    movies = await GetAllMoviesAsync(page, pageSize);
                }
                else
                {
                    movies = await _unitOfWork.MovieRepository!.GetAllAsync(
                    filter: m => m.MovieCategories.Any(mc => mc.CategoryId == categoryId),
                    orderBy: q => q.OrderByDescending(movie => movie.ViewNumber),
                    page: page,
                    pageSize: pageSize
                    );
                }
            }
            if (type == "view")
            {
                if (categoryId == 0)
                {
                    movies = await _unitOfWork.MovieRepository!.GetAllAsync(
                    orderBy: q => q.OrderByDescending(movie => movie.ViewNumber),
                    page: page,
                    pageSize: pageSize
                    );
                }
                else
                {
                    movies = await _unitOfWork.MovieRepository!.GetAllAsync(
                    filter: m => m.MovieCategories.Any(mc => mc.CategoryId == categoryId),
                    orderBy: q => q.OrderByDescending(movie => movie.ViewNumber),
                    page: page,
                    pageSize: pageSize
                    );
                }
            }
            //if (type == "rating")
            //{

            //}
            return movies;
        }

    }
}
