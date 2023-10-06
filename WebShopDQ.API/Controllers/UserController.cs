﻿using Microsoft.AspNetCore.Authorization;
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

        [HttpPut("[action]")]
        //[Authorize(Roles = "User, Manager, Shipper")]
        public async Task<IActionResult> Update(UserInfoDTO model)
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

        [HttpDelete("[action]")]
        //[Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userDelete = await _userService.Delete(id);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Delete user successfully." });
        }
    }
}
