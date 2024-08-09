using AutoMapper;
using Backend.Models;
using Backend.Repositories;
using Backend.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.Numerics;

namespace Backend.Services
{
    public class EpisodeService : IEpisodeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EpisodeService(IUnitOfWork unitOfWork, IMapper mapper) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddEpisodeAsync(EpisodeViewModel episodeViewModel)
        {
            episodeViewModel.Created = DateTime.Now;
            Episode episode = _mapper.Map<Episode>(episodeViewModel);
            await _unitOfWork.EpisodeRepository!.AddAsync(episode);
            await _unitOfWork.CompleteAsync();
            if(episodeViewModel.LinkEmbed_1 != null)
            {
                EpisodeServer episodeServer = new EpisodeServer { EpisodeId = episode.EpisodeId, ServerId = 1, LinkEmbed = episodeViewModel.LinkEmbed_1 };
                await _unitOfWork.EpisodeServerRepository!.AddAsync(episodeServer);
            }
            if(episodeViewModel.LinkEmbed_2 != null)
            {
                EpisodeServer episodeServer = new EpisodeServer { EpisodeId = episode.EpisodeId, ServerId = 2, LinkEmbed = episodeViewModel.LinkEmbed_1 };
                await _unitOfWork.EpisodeServerRepository!.AddAsync(episodeServer);
            }
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateEpisodeAsync(int episode_id, EpisodeViewModel episodeViewModel)
        {
            Episode episode = await _unitOfWork.EpisodeRepository!.GetByIdAsync(episode_id);
            if(episode == null)
            {
                throw new Exception("Episode not found!");
            }
            episodeViewModel.Modified = DateTime.Now;
            _mapper.Map(episodeViewModel, episode);
            _unitOfWork.EpisodeRepository.Update(episode);

            if(!episodeViewModel.LinkEmbed_1.IsNullOrEmpty())
            {
                IEnumerable<EpisodeServer> episodeServers_1 = await _unitOfWork.EpisodeServerRepository!.FindAsync(es => es.EpisodeId == episode_id && es.ServerId == 1);
                EpisodeServer episodeServer_1 = episodeServers_1.FirstOrDefault()!;
                if (episodeServer_1.LinkEmbed != episodeViewModel.LinkEmbed_1)
                {
                    episodeServer_1.LinkEmbed = episodeViewModel.LinkEmbed_1;
                    _unitOfWork.EpisodeServerRepository.Update(episodeServer_1);
                }
            }
            if (!episodeViewModel.LinkEmbed_2.IsNullOrEmpty())
            {
                IEnumerable<EpisodeServer> episodeServers_2 = await _unitOfWork.EpisodeServerRepository!.FindAsync(es => es.EpisodeId == episode_id && es.ServerId == 2);
                EpisodeServer episodeServer_2 = episodeServers_2.FirstOrDefault()!;
                if (episodeServer_2.LinkEmbed != episodeViewModel.LinkEmbed_2)
                {
                    episodeServer_2.LinkEmbed = episodeViewModel.LinkEmbed_2;
                    _unitOfWork.EpisodeServerRepository.Update(episodeServer_2);
                }
            }
            await _unitOfWork.CompleteAsync();
        }
        
        public async Task<Episode> GetEpisodeByIdAsync(int episode_id)
        {
            Episode episode = await _unitOfWork.EpisodeRepository!.GetByIdAsync(episode_id);
            return episode;
        }

        public async Task<IEnumerable<Episode>> GetAllEpisodeAsync(int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 10) pageSize = 10;
            IEnumerable<Episode> episodes = await _unitOfWork.EpisodeRepository!.GetAllAsync(filter:null, orderBy: e=>e.OrderByDescending(ep => ep.EpisodeId), page, pageSize);
            return episodes;
        }

        public async Task DeleteEpisodeByIdAsync(int episode_id)
        {
            Episode episode = await _unitOfWork.EpisodeRepository!.GetByIdAsync(episode_id);
            if (episode == null)
            {
                throw new Exception("Episode not found!");
            }
            _unitOfWork.EpisodeRepository.Remove(episode);
            await _unitOfWork.CompleteAsync();
        }
    }
}
