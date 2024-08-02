using Backend.Models;
using Backend.ViewModels;

namespace Backend.Repositories
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        public Task<Account?> GetAccountByEmailAsync(string username);
        public Task RegisterAdminAsync(RegisterViewModel registerViewModel);
        public Task<bool> CheckExistEmailAsync(string email);
    }
}
