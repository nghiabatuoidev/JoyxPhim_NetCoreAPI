using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.ViewModels
{
    public class MovieViewModel
    {

        public int MovieId { get; set; }

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

        [Required]
        public int? TypeId { get; set; }

        [Required]
        public int? StatusId { get; set; }

        [Required]
        public bool? IsSubDocquyen { get; set; } = false;

        [Required]
        public bool? IsChieurap { get; set; } = false;

        [Required]
        public bool? IsTrending { get; set; } = false;

        [Required]
        public string? GenreMovie { get; set; }

        [Required]
        [MaxLength(100)]
        public string? MovieTime { get; set; }

        [Required]
        [MaxLength(100)]
        public string? EpisodeCurrent { get; set; }

        [Required]
        [MaxLength(100)]
        public string? EpisodeTotal { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Quality { get; set; }

        [Required]
        public int? LangId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? CountryName { get; set; }

        [Required]
        public int? YearReleaseId { get; set; }


        public int? ViewNumber { get; set; }

        [MaxLength(100)]
        public string? TrailerUrl { get; set; }


        public DateTime? Created { get; set; }


        public DateTime? Modified { get; set; }
    }
}
