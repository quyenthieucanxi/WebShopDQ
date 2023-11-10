using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShopDQ.App.Common;
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

        [HttpPost("[action]/{followingId}")]
        //[Authorize(Roles = "User, Seller")]
        public async Task<IActionResult> Follow(Guid followingId)
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            await _friendshipService.Follow(userId, followingId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Follow successfully." });
        }

        [HttpDelete("[action]/{followingId}")]
        //[Authorize(Roles = "User, Seller")]
        public async Task<IActionResult> UnFollow(Guid followingId)
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            await _friendshipService.UnFollow(userId, followingId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Unfollow successfully." });
        }
    }
}
