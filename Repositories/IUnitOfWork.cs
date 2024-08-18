using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
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

        public Task<int> CompleteAsync();
    }
}
