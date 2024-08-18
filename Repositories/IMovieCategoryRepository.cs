using Backend.Models;
using Microsoft.Data.SqlClient.DataClassification;

namespace Backend.Repositories
{
    public interface IMovieCategoryRepository : IGenericRepository<MovieCategory>
    {
        public Task IncludeCategoryAsync(MovieCategory movieCategory);

    }
}
