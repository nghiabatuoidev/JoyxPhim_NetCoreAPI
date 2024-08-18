using Backend.Models;

namespace Backend.Repositories
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(JoyxphimContext dbContext) : base(dbContext) { }
    }
}
