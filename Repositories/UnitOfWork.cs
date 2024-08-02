using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly JoyxphimContext _dbContext;
        public IAccountRepository? AccountRepository { get; }
        public IMovieRepository? MovieRepository { get; }

        public UnitOfWork(JoyxphimContext dbContext)
        {
            _dbContext = dbContext;
            AccountRepository = new AccountRepository(dbContext);
            MovieRepository = new MovieRepository(dbContext);
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
