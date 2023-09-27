using Microsoft.AspNetCore.Identity;
using WebShopDQ.App.Models.Authentication;

namespace WebShopDQ.App.Repositories.IRepositories
{
    public interface IAuthenticationRepository
    {
        Task<IdentityResult> Register(RegisterModel registerModel, string role);
        Task<LoginViewModel> Login(LoginModel loginModel);
        Task<LoginViewModel> NewToken(LoginViewModel loginViewModel);
    }
}
