using System.ComponentModel.DataAnnotations;

namespace Backend.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6)]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6)]
        public string? PasswordAgain { get; set; }

    }
}
