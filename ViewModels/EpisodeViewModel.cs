using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.ViewModels
{
    public class EpisodeViewModel
    {
        [Required]
        public string? LinkEmbed_1 { get; set; }

        public string? LinkEmbed_2 { get; set; }
       
        [Required]
        [RegularExpression(@"^\d+$|^(?i)Full$", ErrorMessage = "The field must be a number or the word 'Full' (case-insensitive).")]
        public string? Name { get; set; }

        [Required]
        [RegularExpression(@"^\d+$|^(?i)Full$", ErrorMessage = "The field must be a number or the word 'Full' (case-insensitive).")]
        public string? Slug { get; set; }

        [Required]
        public string? EpisodeName { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }
    }
}
