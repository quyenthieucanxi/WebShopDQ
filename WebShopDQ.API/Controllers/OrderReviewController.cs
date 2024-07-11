using Microsoft.AspNetCore.Mvc;
using WebShopDQ.App.Common;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Services;
using WebShopDQ.App.Services.IServices;

namespace WebShopDQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderReviewController : ControllerBase
    {
        private readonly ITokenInfoService _tokenInfoService;
        private readonly IOrderReviewService _orderReviewService;

        public OrderReviewController(ITokenInfoService tokenInfoService, IOrderReviewService orderReviewService)
        {
            _tokenInfoService = tokenInfoService;
            _orderReviewService = orderReviewService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(OrderReviewDTO orderReviewDTO)
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            await _orderReviewService.Create(orderReviewDTO, userId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Create OrderReview successfully." });
        }

        [HttpGet("[action]/{url}")]
        public async Task<IActionResult> CountOrderReview(string url)
        {
            var valueReviewsVM =  await _orderReviewService.CountOrderReview(url);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Count OrderReview successfully.", Data= valueReviewsVM });

        }
        [HttpGet("[action]/{pathPost}")]
        public async Task<IActionResult> CountProductReview(string pathPost)
        {
            var valueReviewsVM = await _orderReviewService.CountProductReview(pathPost);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Count OrderReview successfully.", Data = valueReviewsVM });

        }
        [HttpGet("[action]/{pathPost}")]
        public async Task<IActionResult> GetOrderReviewsByProduct(string pathPost,int? rating)
        {
            var OrderReviewsVM = await _orderReviewService.GetOrderReviews(pathPost,rating);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Get OrderReviews successfully.", Data = OrderReviewsVM });

        }
        [HttpGet("[action]/{url}")]
        public async Task<IActionResult> GetOrderReviewsByUser(string url)
        {
            var OrderReviewsVM = await _orderReviewService.GetReviewsUser(url);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Get UserReviews successfully.", Data = OrderReviewsVM });

        }
    }
}
