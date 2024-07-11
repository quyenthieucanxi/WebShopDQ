using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShopDQ.App.Common;
using WebShopDQ.App.Models;
using WebShopDQ.App.Services;
using WebShopDQ.App.Services.IServices;

namespace WebShopDQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendshipController : ControllerBase
    {
        private readonly IFriendshipService _friendshipService;
        private readonly ITokenInfoService _tokenInfoService;

        public FriendshipController(IFriendshipService friendshipService, ITokenInfoService tokenInfoService)
        {
            _friendshipService = friendshipService;
            _tokenInfoService = tokenInfoService;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> CheckFollow(string url)
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var isFollow = await _friendshipService.CheckFollow(infoToken.UserId, url);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "CheckFollow success", Data = isFollow });
        }
        [HttpGet("[action]/{url}")]
        public async Task<IActionResult> CountFollower(string url)
        {
            var countFollower = await _friendshipService.CountFollower(url);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Count Follower success", Data = countFollower });
            }
        [HttpGet("[action]/{url}")]
        public async Task<IActionResult> CountFollowing(string url)
        {
            var countFollowing = await _friendshipService.CountFollowing(url);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Count Following success", Data = countFollowing });
        }

        [HttpPost("[action]/{url}")]
        public async Task<IActionResult> Follow(string url)
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            await _friendshipService.Follow(userId, url);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Follow successfully." });
        }

        [HttpDelete("[action]/{url}")]
        public async Task<IActionResult> UnFollow(string url)
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            await _friendshipService.UnFollow(userId, url);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Unfollow successfully." });
        }
    }
}
