using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Backend.Models;

namespace Backend.ViewModels
{
    public class EpisodeViewModelResponse
    {
        public int? EpisodeId { get; set; }

        public int? MovieId { get; set; }

        public string? Name { get; set; }

        public string? Slug { get; set; }

        public string? EpisodeName { get; set; }

        public List<LinkEpisodeViewModel>? linkEpisodes { get; set; } = new List<LinkEpisodeViewModel>();

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }
    }
}
