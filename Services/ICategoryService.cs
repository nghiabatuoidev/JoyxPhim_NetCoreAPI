using Backend.Models;
using Backend.ViewModels;

namespace Backend.Services
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryViewModelResponse>> GetAllCategoryAsync();
    }
}
