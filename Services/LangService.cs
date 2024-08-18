using AutoMapper;
using Backend.Models;
using Backend.Repositories;
using Backend.ViewModels;

namespace Backend.Services
{
    public class LangService : ILangService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LangService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public  async Task<IEnumerable<LangViewModelReponse>> GetAllLangAsync()
        {

            // Lấy tất cả các danh mục từ repository
            IEnumerable<Lang> langs = await _unitOfWork.LangRepository!.GetAllAsync(filter: null, orderBy: l=>l.OrderBy(lang=> lang.Value),page: 0, pageSize: 0);

            // Ánh xạ các danh mục thành danh sách CategoryViewModelResponse
            IEnumerable<LangViewModelReponse> result = _mapper.Map<IEnumerable<LangViewModelReponse>>(langs);

            // Trả về danh sách đã ánh xạ
            return result;
        }
    }
}
