using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShopDQ.App.Common;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;
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
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Get my info user successfully.", Data = user });
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> CheckUserByEmail(string email)
        {
            await _userService.CheckUserByEmail(email);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "User Exist." });
        }

        [HttpPut("[action]")]
        //[Authorize(Roles = "User, Manager, Shipper")]
        public async Task<IActionResult> UpdateInfo(UserInfoDTO model)
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            await _userService.Update(userId, model);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Update info user successfully." });
        }

        [HttpGet("[action]")]
        //[Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAll(int page, int limit)
        {
            var userList = await _userService.GetAll(page, limit);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Get all user successfully.", Data = userList });
        }

        [HttpDelete("[action]/{userId}")]
        //[Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            var userDelete = await _userService.Delete(userId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Delete user successfully." });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddLikePost(Guid postId)
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            await _userService.AddLikePost(userId,postId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Add Post successfully" });
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSavesPost()
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            var listSavePosts = await _userService.GetSavesPost(userId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Get list save posts successfully.", Data = listSavePosts });
        }
        [HttpDelete("[action]")]
        //[Authorize(Roles = "User, Manager, Shipper")]
        public async Task<IActionResult> RemoveSavesPost(Guid postId)
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            await _userService.RemoveSavesPost(userId,postId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Remove save posts successfully." });
        }
        [HttpGet("[action]")]
        //[Authorize(Roles = "User, Manager, Shipper")]
        public async Task<IActionResult> CheckSavesPost(String pathPost)
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            await _userService.CheckSavesPost(userId, pathPost);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Post exists" });
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddAddressShipping(AddressShippingDTO addressShippingDTO)
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            await _userService.CreateAddRessShipping(userId, addressShippingDTO);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Add AddressShipping successfully" });
        }
        [HttpGet("[action]")]
        //[Authorize(Roles = "User, Manager, Shipper")]
        public async Task<IActionResult> GetAddressShopping()
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            var listAddressShopping = await _userService.GetAddressShopping(userId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Get list AddressShopping successfully.", Data = listAddressShopping });
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> RemoveAddressShopping(Guid addressShippingId)
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            await _userService.RemoveAddressShopping(userId, addressShippingId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Remove AddressShipping successfully." });
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateAddressShopping(Guid addressShippingId,AddressShippingDTO addressShippingDTO)
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            await _userService.UpdateAddressShopping(userId,addressShippingId, addressShippingDTO);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Update AddressShipping successfully." });
        }
        [HttpGet("[action]")]
        //[Authorize(Roles = "User, Manager, Shipper")]
        public async Task<IActionResult> GetAddressShoppingDeFault()
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            var address = await _userService.GetAddressShoppingDeFault(userId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Get AddressShopping Default successfully.", Data = address });
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> SetAddressShopping(Guid addressShippingId)
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            await _userService.SetAddressShopping(userId,addressShippingId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Update AddressShipping Default successfully." });
        }
    }
}
