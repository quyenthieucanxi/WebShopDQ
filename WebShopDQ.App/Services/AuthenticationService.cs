using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebShopDQ.App.Common;
using WebShopDQ.App.Common.Exceptions;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.Services.IServices;
using WebShopDQ.App.ViewModels;
using WebShopDQ.App.ViewModels.Authentication;

namespace WebShopDQ.App.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;

        public AuthenticationService(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }

        public async Task<IdentityResult> Register(RegisterModel registerModel, string role)
        {
            return await _authenticationRepository.Register(registerModel, role);
        }

        public async Task<LoginViewModel> Login(LoginModel loginModel)
        {
            return await _authenticationRepository.Login(loginModel);
        }

        public async Task<LoginViewModel> NewToken(LoginViewModel loginViewModel)
        {
            return await _authenticationRepository.NewToken(loginViewModel);
        }

        public async Task<LinkedEmailModel> GetConfirmEmail(string email)
        {
            return await _authenticationRepository.GetConfirmEmail(email);
        }

        public async Task<bool> ConfirmEmail(string token, string email)
        {
            return await _authenticationRepository.ConfirmEmail(token, email);
        }

        public async Task<LinkedEmailModel> ForgetPassword(ForgetPasswordModel model)
        {
            return await _authenticationRepository.ForgetPassword(model);
        }

        public async Task<IdentityResult> ChangePassword(Guid userId, string oldPassword, string newPassword)
        {
            return await _authenticationRepository.ChangePassword(userId, oldPassword, newPassword);
        }
    }
}
