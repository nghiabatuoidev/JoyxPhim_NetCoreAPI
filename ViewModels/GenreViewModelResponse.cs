using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.ViewModels
{
    public class GenreViewModelResponse
    {
        public int TypeId { get; set; }

        public string? Value { get; set; }
    }
}
