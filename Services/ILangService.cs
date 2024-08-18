using Backend.Models;
using Backend.ViewModels;

namespace Backend.Services
{
    public interface ILangService
    {
        public Task<IEnumerable<LangViewModelReponse>> GetAllLangAsync();
    }
}
