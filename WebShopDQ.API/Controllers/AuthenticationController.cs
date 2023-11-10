﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using WebShopDQ.App.Common;
using WebShopDQ.App.Models;
using WebShopDQ.App.Services.IServices;
using WebShopDQ.App.ViewModels.Authentication;

namespace WebShopDQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IEmailService _emailService;
        private readonly ITokenInfoService _tokenInfoService;

        public AuthenticationController(IAuthenticationService authenticationService, IEmailService emailService,
            ITokenInfoService tokenInfoService)
        {
            _authenticationService = authenticationService;
            _emailService = emailService;
            _tokenInfoService = tokenInfoService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Resgister([FromBody] RegisterModel registerModel)
        {
            await _authenticationService.Register(registerModel);
            var email = await _authenticationService.GetConfirmEmail(registerModel.Email);
            await _emailService.SendEmailRegister(email.Email!, email.Link!);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "User created & Email sent to verify account!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var result = await _authenticationService.Login(loginModel);
            return StatusCode(StatusCodes.Status200OK,
                        new LoginViewModel { AccessToken = result.AccessToken , RefreshToken= result.RefreshToken  });
        }
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordModel model)
        {
            var mail = await _authenticationService.ForgetPassword(model);
            await _emailService.SendEmailForgetPassword(mail.Email!, mail.Link!);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Message = "User forget password & Email sent to successfully!" });
        }

        [HttpGet("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            try
            {
                var result = await _authenticationService.ConfirmEmail(token, email);
                return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Message = "Email verified successfully!" });
            }
            catch
            {
                return StatusCode(StatusCodes.Status404NotFound,
                new Response { Status = "Error", Message = "This user not exist!" });
            }
        }
        [HttpGet("confirmEmailForgetPassword")]
        public async Task<IActionResult> ConfirmEmailForgetPassword(string token, string email,string newPassword)
        {
            try
            {
                var result = await _authenticationService.ConfirmEmailForgetPassword(token, email,newPassword);
                return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Message = "Email verified successfully!" });
            }
            catch
            {
                return StatusCode(StatusCodes.Status404NotFound,
                new Response { Status = "Error", Message = "This user not exist!" });
            }
        }
        [HttpPost("newToken")]
        public async Task<IActionResult> NewToken(LoginViewModel loginViewModel)
        {
            var result = await _authenticationService.NewToken(loginViewModel);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Data = result });
        }


        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword)
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            //await _userAccountService.GetById(userId);
            await _authenticationService.ChangePassword(userId, currentPassword, newPassword);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Change password successfully!" });
        }
    }
}
