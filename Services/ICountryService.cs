using Backend.Models;
using Backend.ViewModels;

namespace Backend.Services
{
    public interface ICountryService
    {
        public Task<IEnumerable<CountryViewModelResponse>> GetAllCountryAsync();
    }
}
