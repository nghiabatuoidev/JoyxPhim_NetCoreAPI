using System.ComponentModel.DataAnnotations;

namespace Backend.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email must end with @gmail.com")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6)]
        [MaxLength(100)]
        public string? Password { get; set; }
    }
}
