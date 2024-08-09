using AutoMapper;
using Backend.Models;
using Backend.ViewModels;
namespace Backend.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieViewModel>();
            CreateMap<MovieViewModel, Movie>().ForMember(dest => dest.MovieId, opt => opt.Ignore()).ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<EpisodeViewModel, Episode>().ForMember(dest => dest.EpisodeId, opt => opt.Ignore()).ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Episode, EpisodeViewModel>();
        }
    }
}
