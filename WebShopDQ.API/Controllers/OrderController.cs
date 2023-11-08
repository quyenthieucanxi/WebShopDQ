using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShopDQ.App.Common;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Services;
using WebShopDQ.App.Services.IServices;

namespace WebShopDQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ITokenInfoService _tokenInfoService;

        public OrderController(IOrderService orderService, ITokenInfoService tokenInfoService)
        {
            _orderService = orderService;
            _tokenInfoService = tokenInfoService;
        }

        [HttpPost("[action]")]
        //[Authorize(Roles = "User, Seller")]
        public async Task<IActionResult> Create(OrderDTO orderDTO)
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            await _orderService.Create(orderDTO, userId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Create order successfully." });
        }

        [HttpGet("[action]")]
        //[Authorize(Roles = "User, Seller")]
        public async Task<IActionResult> GetByStatus(int page, int limit, string status)
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            var orderList = await _orderService.GetByStatus(page, limit, status, userId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Get orders successfully.", Data = orderList });
        }
    }
}
