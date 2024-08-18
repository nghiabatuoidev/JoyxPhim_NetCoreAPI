using AutoMapper;
using Backend.Models;
using Backend.Repositories;
using Backend.ViewModels;

namespace Backend.Services
{
    public class StatusService : IStatusService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public StatusService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StatusViewModelResponse>> GetAllStatusAsync()
        {
            // Lấy tất cả các danh mục từ repository
            IEnumerable<Status> categories = await _unitOfWork.StatusRepository!.GetAllAsync(filter: null, orderBy: s=>s.OrderBy(status=>status.Value), page: 0, pageSize: 0);

            // Ánh xạ các danh mục thành danh sách CategoryViewModelResponse
            IEnumerable<StatusViewModelResponse> result = _mapper.Map<IEnumerable<StatusViewModelResponse>>(categories);

            // Trả về danh sách đã ánh xạ
            return result;
        }
    }
}
