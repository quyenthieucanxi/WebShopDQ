using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShopDQ.App.Common;
using WebShopDQ.App.Services;
using WebShopDQ.App.Services.IServices;

namespace WebShopDQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly ITokenInfoService _tokenInfoService;
        public ChatsController(IChatService chatService, ITokenInfoService tokenInfoService)
        {
            _chatService = chatService;
            _tokenInfoService = tokenInfoService;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetChats(bool? status)
        {
            var token = await _tokenInfoService.GetTokenInfo();
            var chatsVM = await _chatService.GetChats(token.UserId,status);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Gets Chats successfully.", Data = chatsVM });

        }
        [HttpGet("[action]")]
        public async Task<IActionResult> CountChatsNotIsRead()
        {
            var token = await _tokenInfoService.GetTokenInfo();
            var chatsVM = await _chatService.GetChats(token.UserId, false);
            var countChats = chatsVM.Count();
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Gets Chats successfully.", Data = countChats });

        }
        [HttpGet("[action]/{url}")]
        public async Task<IActionResult> GetHistoryChat(string url)
        {
            var token = await _tokenInfoService.GetTokenInfo();
            var chatsVM = await _chatService.GetHistoryChat(token.UserId,url);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Gets History Chat successfully.", Data = chatsVM });

        }
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateIsRead(Guid id)
        {
            var info =  await _tokenInfoService.GetTokenInfo();
            var isRead = await _chatService.UpdateIsRead(id,info.UserId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Update IsRead successfully.", Data = isRead });

        }

    }
}
