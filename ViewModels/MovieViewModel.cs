
using System.ComponentModel.DataAnnotations;

namespace Backend.ViewModels
{
    public class MovieViewModel
    {
        [Required]
        [MaxLength(100)]
        public string? MovieName { get; set; }

        [MaxLength(100)]
        public string? MovieOriginName { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Slug { get; set; }

        public string? MovieContent { get; set; }

        [MaxLength(200)]
        public string? ThumbUrl { get; set; }

        public string? EpisodeTotal { get; set; }

        [Required]
        public int? TypeId { get; set; }

        [Required]
        public int? StatusId { get; set; }

        public bool? IsSubDocquyen { get; set; } = false;

        public bool? IsChieurap { get; set; } = false;

        public bool? IsTrending { get; set; } = false;

        [MaxLength(100)]
        public string? MovieTime { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Quality { get; set; }

        [Required]
        public int? LangId { get; set; }

        [Required]
        public int? YearReleaseId { get; set; }

        [MaxLength(100)]
        public string? TrailerUrl { get; set; }

        public string? ActorName { get; set; }

        public string? DirectorName { get; set; }
        [Required]
        public List<int> Country_ids { get; set; } = new List<int>();

        [Required]
        public List<int> Category_ids { get; set; } = new List<int>();
        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }
    }
}
