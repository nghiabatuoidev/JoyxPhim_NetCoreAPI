using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.ViewModels
{
    public class CountryViewModelResponse
    {

        public int CountryId { get; set; }

        public string? CountryName { get; set; }

        public string? Slug { get; set; }
    }
}
