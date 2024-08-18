using AutoMapper;
using Backend.Models;
using Backend.Repositories;
using Backend.ViewModels;

namespace Backend.Services
{
    public class YearReleaseService : IYearReleaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public YearReleaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<YearViewModelResponse>> GetAllYearReleasesAsync()
        {
            // Lấy tất cả các danh mục từ repository
            IEnumerable<YearRelease> yearReleases = await _unitOfWork.YearReleaseRepository!.GetAllAsync(filter: null, orderBy: y => y.OrderByDescending(year=> year.NumberYear), page: 0, pageSize: 0);

            // Ánh xạ các danh mục thành danh sách CategoryViewModelResponse
            IEnumerable<YearViewModelResponse> result = _mapper.Map<IEnumerable<YearViewModelResponse>>(yearReleases);

            // Trả về danh sách đã ánh xạ
            return result;
        }
    }
}
