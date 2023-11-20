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
        private readonly IFileService _fileService;

        public PostController(IPostService postService, ITokenInfoService tokenInfoService, IFileService fileService)
        {
            _postService = postService;
            _tokenInfoService = tokenInfoService;
            _fileService = fileService;
        }

        [HttpPost("[action]")]
        //[Authorize(Roles = "User, Seller")]
        public async Task<IActionResult> Create(PostDTO postDTO)
        {

            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            await _postService.Create(postDTO, userId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Create post successfully." });
        }
        [HttpGet("[action]")]
        //[Authorize(Roles = "User, Seller")]
        public async Task<IActionResult> GetAll()
        {
            var postList = await _postService.GetAll();
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Get all post successfully.", Data = postList });
        }

        [HttpGet("[action]")]
        //[Authorize(Roles = "User, Seller")]
        public async Task<IActionResult> GetAllByItemPage(int page, int limit)
        {
            var postList = await _postService.GetAllByItemPage(page, limit);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Get all post successfully.",Data = postList });
        }

        [HttpGet("[action]")]
        //[Authorize(Roles = "User, Seller")]
        public async Task<IActionResult> GetByStatus(int page, int limit, string status)
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            var postList = await _postService.GetByStatus(page, limit, status, userId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Get posts successfully.", Data = postList });
        }

        [HttpPut("[action]/{idPost}")]
        //[Authorize(Roles = "Manager")]
        public async Task<IActionResult> UpdateStatus(Guid idPost)
        {
            await _postService.UpdateStatus(idPost);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Update post successfully." });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetById(Guid postId)
        {
            var post = await _postService.GetById(postId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Get detail post successfully.", Data = post });
        }
    }
}
