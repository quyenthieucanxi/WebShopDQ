using Microsoft.AspNetCore.Identity;
using WebShopDQ.App.Models;
using WebShopDQ.App.ViewModels;
using WebShopDQ.App.ViewModels.Authentication;

namespace WebShopDQ.App.Services.IServices
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> Register(RegisterModel registerModel);
        Task<LoginViewModel> Login(LoginModel loginModel);
        Task<LoginViewModel> NewToken(LoginViewModel loginViewModel);
        Task<LinkedEmailModel> GetConfirmEmail(string email);
        Task<LinkedEmailModel> GetConfirmEmailForgetPassword(string email, User user, string newPassword);
        Task<bool> ConfirmEmail(string token, string email);
        Task<LinkedEmailModel> ForgetPassword(ForgetPasswordModel model);
        Task<IdentityResult> ChangePassword(Guid userId, string oldPassword, string newPassword);
        Task<bool> ConfirmEmailForgetPassword(string token, string email, string newPassword);
    }
}
