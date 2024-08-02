using Backend.Models;
using Backend.ViewModels;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(JoyxphimContext dbContext) : base(dbContext)
        {
        }

        public async Task<Account?> GetAccountByEmailAsync(string username)
        {
            Account? account = await _dbContext.Accounts.SingleOrDefaultAsync(a => a.Email == username);
            return account;
        }

        public async Task RegisterAdminAsync(RegisterViewModel registerViewModel)
        {
            Account newAccount = new Account
            {
                Email = registerViewModel.Email,
                Password = registerViewModel.Password,
                Role = "Admin"
            };
            await _dbContext.Accounts.AddAsync(newAccount);
        }

        public async Task<bool> CheckExistEmailAsync(string email)
        {
            return await _dbContext.Accounts.AnyAsync(a => a.Email == email);
        }
       
    }
}
