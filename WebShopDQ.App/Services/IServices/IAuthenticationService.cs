using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Models.Authentication;

namespace WebShopDQ.App.Services.IServices
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> Register(RegisterModel registerModel, string role);
        Task<LoginViewModel> Login(LoginModel loginModel);
        Task<LoginViewModel> NewToken(LoginViewModel loginViewModel);
    }
}
