using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public IAccountRepository? AccountRepository { get; }
        public IMovieRepository? MovieRepository { get; }


        public Task<int> CompleteAsync();
    }
}
