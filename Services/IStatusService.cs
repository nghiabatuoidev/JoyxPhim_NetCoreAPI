using Backend.ViewModels;

namespace Backend.Services
{
    public interface IStatusService
    {
        public Task<IEnumerable<StatusViewModelResponse>> GetAllStatusAsync();
    }
}
