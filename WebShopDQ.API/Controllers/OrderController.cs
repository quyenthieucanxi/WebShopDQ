using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShopDQ.App.Common;
using WebShopDQ.App.Common.Constant;
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
        private readonly IPaymentService _paymentService;
        private readonly ITokenInfoService _tokenInfoService;
  

        public OrderController(IOrderService orderService
            , ITokenInfoService tokenInfoService, 
            IPaymentService paymentService 
            )
        {
            _orderService = orderService;
            _tokenInfoService = tokenInfoService;
            _paymentService = paymentService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(OrderDTO orderDTO)
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            await _orderService.Create(orderDTO, userId);

            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Create order successfully." });
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUrlPayment(OrderDTO orderDTO)
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId; 
            var urlPayment = await _paymentService.CreateUrlPayment(orderDTO, userId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Create UrlPayment successfully.", Data = urlPayment });
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> CallBackPayment([FromQuery] VNPayDTO vNPayDTO)
        {
            await _tokenInfoService.GetTokenInfo();
            var paymentViewModel = await _paymentService.CallbackPayment(vNPayDTO);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Payment Success" ,Data = paymentViewModel });
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetById(Guid OrderId)
        {
            await _tokenInfoService.GetTokenInfo();
            var order = await _orderService.GetById(OrderId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Get order successfully.", Data = order });
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByStatusByPage(int page, int limit, string status)
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            var orderList = await _orderService.GetByStatus(page, limit, status, userId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Get orders successfully.", Data = orderList });
        }
        [HttpGet("[action]")]
        //[Authorize(Roles = "User, Seller")]
        public async Task<IActionResult> GetByStatus(string status)
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            var orderList = await _orderService.GetByStatus(status, userId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Get orders successfully.", Data = orderList });
        }
        [HttpGet("[action]")]
        //[Authorize(Roles = "User, Seller")]
        public async Task<IActionResult> GetAll()
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            var orderList = await _orderService.GetAll(userId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Get orders successfully.", Data = orderList });
        }
        [HttpGet("[action]")]
        //[Authorize(Roles = "User, Seller")]
        public async Task<IActionResult> GetAllBySeller()
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            var orderList = await _orderService.GetAllBySeller(userId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Get orders successfully.", Data = orderList });
        }
        [HttpPut("[action]")]
        //[Authorize(Roles = "User, Seller")]
        public async Task<IActionResult> UpdateStatus(string status,Guid orderId)
        {
            var info = await _tokenInfoService.GetTokenInfo();
            await _orderService.UpdateStatus(orderId,status,info.UserId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Update orders successfully." });
        }
    }
}
