using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShopDQ.App.Common;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories;
using WebShopDQ.App.Services;
using WebShopDQ.App.Services.IServices;

namespace WebShopDQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ITokenInfoService _tokenInfoService;

        public PostController(IPostService postService, ITokenInfoService tokenInfoService)
        {
            _postService = postService;
            _tokenInfoService = tokenInfoService;

        }

        [HttpPost("[action]")]
        //[Authorize(Roles = "User, Seller")]
        public async Task<IActionResult> Create(PostDTO postDTO)
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            var post = await _postService.Create(postDTO, userId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Create post successfully." });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll(int page, int limit)
        {
            var postList = await _postService.GetAll(page, limit);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Get all post successfully.",Data = postList });
        }
    }
}
