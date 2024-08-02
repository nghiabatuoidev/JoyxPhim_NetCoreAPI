using Backend.ViewModels;

namespace Backend.Services
{
    public interface IJwtService
    {
        public string GenerateAccessToken(int accountId);
    }
}
