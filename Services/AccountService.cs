using Backend.Models;
using Backend.Repositories;
using Backend.ViewModels;
using System.Data;

namespace Backend.Services
{
    public class AccountService : IAccountService
    {
        private IUnitOfWork _unitOfWork;
        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async  Task<AccountViewModel> LoginAdminAsync(LoginViewModel loginViewModel)
        {
            bool isCheckExistEmail = await _unitOfWork.AccountRepository!.CheckExistEmailAsync(loginViewModel.Email!);
            if (isCheckExistEmail)
            {
                Account? account = await _unitOfWork.AccountRepository.GetAccountByEmailAsync(loginViewModel.Email!);

                bool isVerifyPassword = DecryptPassword(loginViewModel.Password, account.Password);
                if (!isVerifyPassword)
                {
                    throw new ArgumentException("Password incorrect!");
                }

                if (account == null)
                {
                    throw new ArgumentException("Email or password incorrect!");
                }

                AccountViewModel accountViewModel = new AccountViewModel
                {
                    AccountId = account.AccountId,
                    FirstName = account.Firstname,
                    LastName = account.Lastname,
                    Picture_url = account.PictureUrl
                };
                return accountViewModel;
            }
            else
            {
                throw new ArgumentException("Email isn't exist!");
            }
        }

        public async Task<AccountViewModel> LoginGoogleAsync(GoogleLoginViewModel googleLoginViewModel)
        {
            bool isExistEmail = await _unitOfWork.AccountRepository!.CheckExistEmailAsync(googleLoginViewModel.Email!);
            AccountViewModel accountViewModel = null;
            if (isExistEmail)
            {
                Account account = await _unitOfWork.AccountRepository?.GetAccountByEmailAsync(googleLoginViewModel.Email);
                if (account != null)
                {
                    accountViewModel = new AccountViewModel
                    {
                        AccountId= account.AccountId,
                        Email = googleLoginViewModel.Email,
                        FullName = googleLoginViewModel.FullName,
                        Picture_url = googleLoginViewModel.PictureUrl,
                        GoogleId = googleLoginViewModel.GoogleId,
                    };
                }
            }
            else
            {
                Account newAccount = new Account
                {
                    Email = googleLoginViewModel.Email,
                    PictureUrl = googleLoginViewModel.PictureUrl,
                    GoogleId = googleLoginViewModel.GoogleId,
                    FullName = googleLoginViewModel.FullName,
                    Role = "Client",
                    Created = DateTime.Now
                };
                await _unitOfWork.AccountRepository.AddAsync(newAccount);
                await _unitOfWork.CompleteAsync();

                Account account = await _unitOfWork.AccountRepository.GetByIdAsync(newAccount.AccountId);
                await _unitOfWork.CompleteAsync();
                accountViewModel = new AccountViewModel
                {
                    AccountId = newAccount.AccountId,
                    Email = googleLoginViewModel.Email,
                    FullName = googleLoginViewModel.FullName,
                    Picture_url = googleLoginViewModel.PictureUrl,
                    GoogleId = googleLoginViewModel.GoogleId,
                };
            }
            return accountViewModel!;
        }

        public async Task RegisterAdminAsync(RegisterViewModel registerViewModel)
        {
            bool isExistEmail = await _unitOfWork.AccountRepository!.CheckExistEmailAsync(registerViewModel.Email!);
            if (isExistEmail)
            {
                throw new ArgumentException("Email name is exist!");
            }
            if (registerViewModel.Password != registerViewModel.PasswordAgain)
            {
                throw new ArgumentException("Password aren't the same!");
            }
            string passwordHashed = EncryptPassword(registerViewModel.Password!);
            registerViewModel.Password = passwordHashed;
            await _unitOfWork.AccountRepository.RegisterAdminAsync(registerViewModel);
            await _unitOfWork.CompleteAsync();

        }

        public string EncryptPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool DecryptPassword(string enterPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enterPassword, hashedPassword);
        }
    }
}