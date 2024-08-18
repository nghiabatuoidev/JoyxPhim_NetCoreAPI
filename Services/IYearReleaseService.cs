using Backend.ViewModels;

namespace Backend.Services
{
    public interface IYearReleaseService
    {
        public Task<IEnumerable<YearViewModelResponse>> GetAllYearReleasesAsync();
    }
}
