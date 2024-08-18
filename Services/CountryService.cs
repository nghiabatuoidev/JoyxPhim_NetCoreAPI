using AutoMapper;
using Backend.Models;
using Backend.Repositories;
using Backend.ViewModels;

namespace Backend.Services
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CountryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CountryViewModelResponse>> GetAllCountryAsync()
        {
            // Lấy tất cả các danh mục từ repository
            IEnumerable<Country> categories = await _unitOfWork.CountryRepostiory!.GetAllAsync(filter: null, orderBy: c => c.OrderBy(country => country.CountryName),page:0, pageSize:0);

            // Ánh xạ các danh mục thành danh sách CategoryViewModelResponse
            IEnumerable<CountryViewModelResponse> result = _mapper.Map<IEnumerable<CountryViewModelResponse>>(categories);

            // Trả về danh sách đã ánh xạ
            return result;
        }
    }
}
