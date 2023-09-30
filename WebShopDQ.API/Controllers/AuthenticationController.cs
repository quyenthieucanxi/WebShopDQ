using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using WebShopDQ.App.Common;
using WebShopDQ.App.Models;
using WebShopDQ.App.Models.Authentication;
using WebShopDQ.App.Services.IServices;

namespace WebShopDQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IEmailService _emailService;

        public AuthenticationController(IAuthenticationService authenticationService, IEmailService emailService)
        {
            _authenticationService = authenticationService;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Resgister([FromBody] RegisterModel registerModel, string role)
        {
            await _authenticationService.Register(registerModel, role);
            var email = await _authenticationService.GetConfirmEmail(registerModel.Email);
            await _emailService.SendEmailRegister(email.Email, email.Link);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            return Ok(await _authenticationService.Login(loginModel));
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

        [HttpPost("newToken")]
        public async Task<IActionResult> NewToken(LoginViewModel loginViewModel)
        {
            return Ok(await _authenticationService.NewToken(loginViewModel));
        }
    }
}
