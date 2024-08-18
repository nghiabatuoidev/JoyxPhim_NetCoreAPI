using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.ViewModels
{
    public class YearViewModelResponse
    {
        public int Id { get; set; }
        public int? NumberYear { get; set; }
    }
}
