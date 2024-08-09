using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.ViewModels
{
    public class EpisodeViewModel
    {
        public int? EpisodeId { get; set; }

        public int? MovieId { get; set; }

        public string? LinkEmbed_1 { get; set; }

        public string? LinkEmbed_2 { get; set; }

        public string? Name { get; set; }

        public string? Slug { get; set; }

        public string? EpisodeName { get; set; }

        public DateTime? Created { get; set; } 

        public DateTime? Modified { get; set; }
    }
}
