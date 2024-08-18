using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly JoyxphimContext _dbContext;
        public IAccountRepository? AccountRepository { get; }
        public IMovieRepository? MovieRepository { get; }
        public IMovieCountryRepository? MovieCountryRepository { get; }
        public IMovieCategoryRepository? MovieCategoryRepository { get; }
        public IEpisodeRepository? EpisodeRepository { get; }
        public IEpisodeServerRepository? EpisodeServerRepository { get; }

        public ICountryRepostiory? CountryRepostiory { get; }

        public ICategoryRepository? CategoryRepository { get; }

        public IStatusRepository? StatusRepository { get; }

        public IGenreRepository? GenreRepository { get; }

        public IYearReleaseRepository? YearReleaseRepository { get; }

        public ILangRepository? LangRepository { get; }

        public UnitOfWork(JoyxphimContext dbContext)
        {
            _dbContext = dbContext;
            AccountRepository = new AccountRepository(dbContext);
            MovieRepository = new MovieRepository(dbContext);
            MovieCategoryRepository = new MovieCategoryRepository(dbContext);
            MovieCountryRepository = new MovieCountryRepository(dbContext);
            EpisodeRepository = new EpisodeRepository(dbContext);
            EpisodeServerRepository = new EpisodeServerRepository(dbContext);
            CountryRepostiory = new CountryRepostiory(dbContext);
            CategoryRepository = new CategoryRepository(dbContext);
            StatusRepository = new StatusRepository(dbContext);
            GenreRepository = new GenreRepository(dbContext);
            YearReleaseRepository = new YearReleaseRepository(dbContext);
            LangRepository = new LangRepository(dbContext);

        }
        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
