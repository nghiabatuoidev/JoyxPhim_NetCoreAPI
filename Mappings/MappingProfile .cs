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
            CreateMap<MovieViewModel, Movie>();

        }
    }
}
