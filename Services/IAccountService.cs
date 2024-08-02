
using Backend.ViewModels;

namespace Backend.Services
{
    public interface IAccountService
    {
        public Task<AccountViewModel> LoginAdminAsync(LoginViewModel loginViewModel);
        public Task<AccountViewModel> LoginGoogleAsync(GoogleLoginViewModel googleLoginViewModel);
        public Task RegisterAdminAsync(RegisterViewModel registerViewModel);
        public string EncryptPassword(string password);
        public bool DecryptPassword(string enterPassword, string hashedPassword);
    }
}
