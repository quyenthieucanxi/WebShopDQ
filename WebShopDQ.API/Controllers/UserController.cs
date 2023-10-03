using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShopDQ.App.Services;
using WebShopDQ.App.Services.IServices;

namespace WebShopDQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenInfoService _tokenInfoService;

        public UserController(IUserService userService, ITokenInfoService tokenInfoService)
        {
            _userService = userService;
            _tokenInfoService = tokenInfoService;
        }

        [HttpGet("[action]")]
        //[Authorize(Roles = "User, Manager, Shipper")]
        public async Task<IActionResult> GetMyInfo()
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            //await _userAccountService.GetById(userId);
            var user = await _userService.GetById(userId);
            return Ok(user);
        }
    }
}
