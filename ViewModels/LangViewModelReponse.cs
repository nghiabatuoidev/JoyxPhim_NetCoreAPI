using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.ViewModels
{
    public class LangViewModelReponse
    {
        public int LangId { get; set; }
        public string? Value { get; set; }
    }
}
