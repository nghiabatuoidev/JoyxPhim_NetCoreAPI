using System.ComponentModel.DataAnnotations;

namespace Backend.ViewModels
{
    public class MovieViewModelResponse
    {
        public int? MovieId { get; set; }
        public string? MovieName { get; set; }
        public string? MovieOriginName { get; set; }
        public string? Slug { get; set; }
        public string? MovieContent { get; set; }
        public string? ThumbUrl { get; set; }
        public bool? IsSubDocquyen { get; set; } = false;
        public bool? IsChieurap { get; set; } = false;
        public bool? IsTrending { get; set; } = false;
        public string? MovieTime { get; set; }
        public string? EpisodeCurrent { get; set; }
        public string? EpisodeTotal { get; set; }
        public string? Quality { get; set; }
        public string? TrailerUrl { get; set; }
        public List<string> CountriesName { get; set; } = new List<string>();
        public List<string> CategoriesName { get; set; } = new List<string>();
        public string? ActorName { get; set; }
        public string? DirectorName { get; set; }
        public string? Status { get; set; }
        public double? Rating { get; set; } = 0;
        public int? ViewNumber { get; set; } = 0;
        public string? TypeValue { get; set; }
        public string? LangValue { get; set; }
        public int? Year { get; set; }
        public int? YearReleaseId { get; set; }
        public int? TypeId { get; set; }
        public int? StatusId { get; set; }
        public int? LangId { get; set; }
        public List<int> Country_ids { get; set; } = new List<int>();
        public List<int> Category_ids { get; set; } = new List<int>();
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
