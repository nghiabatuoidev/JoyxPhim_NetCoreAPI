
using AutoMapper;
using Backend.Models;
using Backend.Repositories;
using Backend.ViewModels;

namespace Backend.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryViewModelResponse>> GetAllCategoryAsync()
        {
            // Lấy tất cả các danh mục từ repository
            IEnumerable<Category> categories = await _unitOfWork.CategoryRepository!.GetAllAsync(filter: null, orderBy: c => c.OrderBy(category => category.CategoryName), page: 0, pageSize: 0);

            // Ánh xạ các danh mục thành danh sách CategoryViewModelResponse
            IEnumerable<CategoryViewModelResponse> result = _mapper.Map<IEnumerable<CategoryViewModelResponse>>(categories);

            // Trả về danh sách đã ánh xạ
            return result;
        }
    }
}
