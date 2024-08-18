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
        public EpisodeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddEpisodeAsync(int movieId, EpisodeViewModel episodeViewModel)
        {
            episodeViewModel.Created = DateTime.Now;
            Movie movie = await _unitOfWork.MovieRepository!.GetByIdAsync(movieId);
            if (movie == null)
            {
                throw new Exception("Movie not found!");

            }
            Episode episode = _mapper.Map<Episode>(episodeViewModel);
            episode.MovieId = movieId;
            await _unitOfWork.EpisodeRepository!.AddAsync(episode);
            await _unitOfWork.CompleteAsync();
            if (!episodeViewModel.LinkEmbed_1.IsNullOrEmpty())
            {
                EpisodeServer episodeServer = new EpisodeServer { EpisodeId = episode.EpisodeId, ServerId = 1, LinkEmbed = episodeViewModel.LinkEmbed_1 };
                await _unitOfWork.EpisodeServerRepository!.AddAsync(episodeServer);
            }
            if (!episodeViewModel.LinkEmbed_2.IsNullOrEmpty())
            {
                EpisodeServer episodeServer = new EpisodeServer { EpisodeId = episode.EpisodeId, ServerId = 2, LinkEmbed = episodeViewModel.LinkEmbed_2 };
                await _unitOfWork.EpisodeServerRepository!.AddAsync(episodeServer);
            }
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateEpisodeAsync(int episode_id, EpisodeViewModel episodeViewModel)
        {
            Episode episode = await _unitOfWork.EpisodeRepository!.GetByIdAsync(episode_id);
            if (episode == null)
            {
                throw new Exception("Episode not found!");
            }
            episodeViewModel.Modified = DateTime.Now;
            _mapper.Map(episodeViewModel, episode);
            _unitOfWork.EpisodeRepository.Update(episode);

            //if (!episodeViewModel.LinkEmbed_1.IsNullOrEmpty())
            //{
            //    IEnumerable<EpisodeServer> episodeServers_1 = await _unitOfWork.EpisodeServerRepository!.FindAsync(es => es.EpisodeId == episode_id && es.ServerId == 1);
            //    EpisodeServer episodeServer_1 = episodeServers_1.FirstOrDefault()!;
            //    if (episodeServer_1.LinkEmbed != episodeViewModel.LinkEmbed_1)
            //    {
            //        episodeServer_1.LinkEmbed = episodeViewModel.LinkEmbed_1;
            //        _unitOfWork.EpisodeServerRepository.Update(episodeServer_1);
            //    }
            //}
            //if (!episodeViewModel.LinkEmbed_2.IsNullOrEmpty())
            //{
            //    IEnumerable<EpisodeServer> episodeServers_2 = await _unitOfWork.EpisodeServerRepository!.FindAsync(es => es.EpisodeId == episode_id && es.ServerId == 2);
            //    EpisodeServer episodeServer_2 = episodeServers_2.FirstOrDefault()!;
            //    if (episodeServer_2.LinkEmbed != episodeViewModel.LinkEmbed_2)
            //    {
            //        episodeServer_2.LinkEmbed = episodeViewModel.LinkEmbed_2;
            //        _unitOfWork.EpisodeServerRepository.Update(episodeServer_2);
            //    }
            //}

            if (episodeViewModel.LinkEmbed_1.IsNullOrEmpty())
            {
                IEnumerable<EpisodeServer> episodeServers_1 = await _unitOfWork.EpisodeServerRepository!.FindAsync(es => es.EpisodeId == episode_id && es.ServerId == 1);
                EpisodeServer episodeServer_1 = episodeServers_1.FirstOrDefault()!;
                if (episodeServer_1 != null)
                {
                    _unitOfWork.EpisodeServerRepository.Remove(episodeServer_1);
                }
            }
            else
            {
                IEnumerable<EpisodeServer> episodeServers_1 = await _unitOfWork.EpisodeServerRepository!.FindAsync(es => es.EpisodeId == episode_id && es.ServerId == 1);

                if (episodeServers_1.IsNullOrEmpty())
                {
                    EpisodeServer episodeServer = new EpisodeServer { EpisodeId = episode.EpisodeId, ServerId = 1, LinkEmbed = episodeViewModel.LinkEmbed_1 };
                    episodeServer.LinkEmbed = episodeViewModel.LinkEmbed_1;
                    await _unitOfWork.EpisodeServerRepository!.AddAsync(episodeServer);
                }
                else
                {
                    EpisodeServer episodeServer_1 = episodeServers_1.FirstOrDefault()!;
                    if (episodeServer_1.LinkEmbed != episodeViewModel.LinkEmbed_1)
                    {
                        episodeServer_1.LinkEmbed = episodeViewModel.LinkEmbed_1;
                        _unitOfWork.EpisodeServerRepository.Update(episodeServer_1);
                    }
                }
            }

            if (episodeViewModel.LinkEmbed_2.IsNullOrEmpty())
            {
                IEnumerable<EpisodeServer> episodeServers_2 = await _unitOfWork.EpisodeServerRepository!.FindAsync(es => es.EpisodeId == episode_id && es.ServerId == 2);
                EpisodeServer episodeServer_2 = episodeServers_2.FirstOrDefault()!;
                if (episodeServer_2 != null)
                {
                    _unitOfWork.EpisodeServerRepository.Remove(episodeServer_2);
                }
            }
            else
            {
                IEnumerable<EpisodeServer> episodeServers_2 = await _unitOfWork.EpisodeServerRepository!.FindAsync(es => es.EpisodeId == episode_id && es.ServerId == 2);
                if (episodeServers_2.IsNullOrEmpty())
                {
                    EpisodeServer episodeServer = new EpisodeServer { EpisodeId = episode.EpisodeId, ServerId = 2, LinkEmbed = episodeViewModel.LinkEmbed_2 };
                    episodeServer.LinkEmbed = episodeViewModel.LinkEmbed_2;
                    await _unitOfWork.EpisodeServerRepository!.AddAsync(episodeServer);
                }
                else
                {
                    EpisodeServer episodeServer_2 = episodeServers_2.FirstOrDefault()!;
                    if (episodeServer_2.LinkEmbed != episodeViewModel.LinkEmbed_2)
                    {
                        episodeServer_2.LinkEmbed = episodeViewModel.LinkEmbed_2;
                        _unitOfWork.EpisodeServerRepository.Update(episodeServer_2);
                    }
                }


            }
            await _unitOfWork.CompleteAsync();
        }

        public async Task<EpisodeViewModelResponse> GetEpisodeByIdAsync(int episode_id)
        {
            Episode episode = await _unitOfWork.EpisodeRepository!.GetByIdAsync(episode_id);
            if (episode == null) { throw new Exception("Episode not found!"); }
            EpisodeViewModelResponse episodeResponse = _mapper.Map<EpisodeViewModelResponse>(episode);
            IEnumerable<EpisodeServer> episodeServers = await _unitOfWork.EpisodeServerRepository!.GetAllAsync(es => es.EpisodeId == episode_id);
            if (episodeServers.Any())
            {
                foreach (EpisodeServer es in episodeServers)
                {
                    LinkEpisodeViewModel linkEpisode = new LinkEpisodeViewModel();
                    if (es != null)
                    {
                        await _unitOfWork.EpisodeServerRepository!.IncludeSeverAsync(es);
                        if (es.Server != null)
                        {

                            linkEpisode.ServerId = es.Server.ServerId;
                            linkEpisode.SeverName = es.Server.Value;
                            linkEpisode.LinkEmbed = es.LinkEmbed;
                        }
                    }
                    episodeResponse.linkEpisodes!.Add(linkEpisode);
                }
            }
            return episodeResponse;
        }

        public async Task<IEnumerable<EpisodeViewModelResponse>> GetAllEpisodeAsync(int movieId)
        {
            IEnumerable<Episode> episodes2 = await _unitOfWork.EpisodeRepository!.GetAllAsync(e => e.MovieId == movieId);
            IEnumerable<EpisodeViewModelResponse> episodesResponse = _mapper.Map<IEnumerable<EpisodeViewModelResponse>>(episodes2);
            if (episodesResponse.IsNullOrEmpty())
            {
                return episodesResponse;
            }
            foreach (var episode in episodesResponse)
            {
                IEnumerable<EpisodeServer> episodeServers = await _unitOfWork.EpisodeServerRepository!.GetAllAsync(es => es.EpisodeId == episode.EpisodeId);
                if (episodeServers.Any())
                {
                    foreach (EpisodeServer es in episodeServers)
                    {
                        LinkEpisodeViewModel linkEpisode = new LinkEpisodeViewModel();
                        if (es != null)
                        {
                            await _unitOfWork.EpisodeServerRepository!.IncludeSeverAsync(es);
                            if (es.Server != null)
                            {
                                linkEpisode.ServerId = es.Server.ServerId;
                                linkEpisode.SeverName = es.Server.Value;
                                linkEpisode.LinkEmbed = es.LinkEmbed;
                            }
                        }
                        episode.linkEpisodes!.Add(linkEpisode);
                    }
                }

            }
            return episodesResponse.OrderBy(e =>
            {
                // Kiểm tra xem slug có phải là số không
                if (int.TryParse(e.Slug, out int number))
                {
                    // Nếu là số, sắp xếp theo số
                    return number;
                }
                else
                {
                    // Nếu không phải số, sắp xếp theo chữ cái
                    return int.MaxValue; // Đảm bảo các giá trị không phải số được sắp xếp sau các số
                }
            })
            .ThenBy(e => e.Slug); // Sắp xếp theo thứ tự chữ cái nếu slug không phải là số

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

        public async Task DeleteRangeEpisodeByIdAsync(List<int> episodeIds)
        {
            List<Episode> episodes = new List<Episode>(); // Sử dụng List<Movie> để dễ dàng thêm phần tử
            foreach (int episodeId in episodeIds)
            {
                Episode episode = await _unitOfWork.EpisodeRepository!.GetByIdAsync(episodeId);
                if (episode != null) // Kiểm tra null để tránh lỗi nếu movie không tồn tại
                {
                    episodes.Add(episode); // Thêm vào danh sách
                }
            }

            // Gọi RemoveRange với danh sách movies
            _unitOfWork.EpisodeRepository!.RemoveRange(episodes);
            await _unitOfWork.CompleteAsync();
        }
    }
}
