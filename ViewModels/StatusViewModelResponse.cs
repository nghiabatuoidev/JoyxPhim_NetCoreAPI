using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.ViewModels
{
    public class StatusViewModelResponse
    {
        public int StatusId { get; set; }
        public string? Value { get; set; }
    }
}
