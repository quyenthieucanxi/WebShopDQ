using Microsoft.AspNetCore.Mvc;
using WebShopDQ.App.Common;
using WebShopDQ.App.Services;
using WebShopDQ.App.Services.IServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebShopDQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotifyController : ControllerBase
    {
        private readonly ITokenInfoService _tokenInfoService;
        private readonly INotifyService _notifierService;

        public NotifyController(
            ITokenInfoService tokenInfoService, 
            INotifyService notifierService)
        {
            _tokenInfoService = tokenInfoService;
            _notifierService = notifierService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByUser(int page, int pageSize, bool? status)
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            var notifies = await _notifierService.GetByUser(userId,page,pageSize,status);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Get notifies successfully.", Data = notifies });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CountNotifiesNotIsRead()
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            var totalNotifiesNotIsRead = await _notifierService.CountNotifiesNotIsRead(userId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Get Total Notifies Not IsRead successfully.", Data = totalNotifiesNotIsRead });
        }
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateIsRead(Guid id)
        {
            var info = await _tokenInfoService.GetTokenInfo();
            var isRead = await _notifierService.UpdateIsRead(id, info.UserId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Update IsRead successfully.", Data = isRead });

        }
    }
}
