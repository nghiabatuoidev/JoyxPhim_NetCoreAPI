using AutoMapper;
using Backend.Models;
using Backend.Repositories;
using Backend.ViewModels;

namespace Backend.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GenreViewModelResponse>> GetAllGenreAsync()
        {
            // Lấy tất cả các danh mục từ repository
            IEnumerable<Genre> categories = await _unitOfWork.GenreRepository!.GetAllAsync(page: 0, pageSize: 0);

            // Ánh xạ các danh mục thành danh sách CategoryViewModelResponse
            IEnumerable<GenreViewModelResponse> result = _mapper.Map<IEnumerable<GenreViewModelResponse>>(categories);

            // Trả về danh sách đã ánh xạ
            return result;
        }
    }
}
