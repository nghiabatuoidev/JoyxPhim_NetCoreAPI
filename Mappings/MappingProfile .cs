using AutoMapper;
using Backend.Models;
using Backend.ViewModels;
namespace Backend.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //movie
            CreateMap<Movie, MovieViewModel>();
            CreateMap<Movie, MovieViewModelResponse>();
            CreateMap<MovieViewModel, Movie>().ForMember(dest => dest.MovieId, opt => opt.Ignore()).ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            //category
            CreateMap<Category, CategoryViewModelResponse>();
            CreateMap<CategoryViewModelResponse, Category>();
            //status
            CreateMap<Status, StatusViewModelResponse>();
            CreateMap<StatusViewModelResponse, Status>();
            //country
            CreateMap<Country, CountryViewModelResponse>();
            CreateMap<CountryViewModelResponse, Country>();
            //Genre
            CreateMap<Genre, GenreViewModelResponse>();
            CreateMap<GenreViewModelResponse, Genre>();
            //Year Release
            CreateMap<YearRelease, YearViewModelResponse>();
            CreateMap<YearViewModelResponse, YearRelease>();
            //Lang
            CreateMap<Lang, LangViewModelReponse>();
            CreateMap<LangViewModelReponse, Lang>();
            //EPISODE
            //episode
            CreateMap<EpisodeViewModelResponse, Episode>();
            CreateMap<Episode, EpisodeViewModelResponse>();

            //episode
            CreateMap<EpisodeViewModelResponse, Episode>();
            CreateMap<Episode, EpisodeViewModelResponse>();
            CreateMap<EpisodeViewModel, Episode>();
            CreateMap<Episode, EpisodeViewModel>();
        }
    }
}
