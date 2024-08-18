using Backend.Models;

namespace Backend.Repositories
{
    public class CountryRepostiory : GenericRepository<Country>, ICountryRepostiory
    {
        public CountryRepostiory(JoyxphimContext dbContext) : base(dbContext) { }
    }
}
