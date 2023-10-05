using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Services;
using WebShopDQ.App.Services.IServices;
using WebShopDQ.App.ViewModels;

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
            var user = await _userService.GetById(userId);
            return Ok(user);
        }

        [HttpPut("[action]")]
        //[Authorize(Roles = "User, Manager, Shipper")]
        public async Task<IActionResult> Update(Guid UserId,UserInfoDTO model)
        {
            //var infoToken = await _tokenInfoService.GetTokenInfo();
            //var userId = infoToken.UserId;
            var user = await _userService.Update(UserId, model);
            return Ok(user);
        }

        [HttpGet("[action]")]
        //[Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAll(int page, int limit)
        {
            //var infoToken = await _tokenInfoService.GetTokenInfo();
            //var userId = infoToken.UserId;
            var userList = await _userService.GetAll(page, limit);
            return Ok(userList);
        }

        [HttpDelete("[action]")]
        //[Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userDelete = await _userService.Delete(id);
            return Ok(userDelete);
        }
    }
}
