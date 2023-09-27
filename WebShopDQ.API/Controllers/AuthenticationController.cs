using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShopDQ.App.Models.Authentication;
using WebShopDQ.App.Services.IServices;

namespace WebShopDQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Resgister([FromBody] RegisterModel registerModel, string role)
        {
            return Ok(await _authenticationService.Register(registerModel, role));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            return Ok(await _authenticationService.Login(loginModel));
        }

        [HttpPost("newToken")]
        public async Task<IActionResult> NewToken(LoginViewModel loginViewModel)
        {
            return Ok(await _authenticationService.NewToken(loginViewModel));
        }
    }
}
