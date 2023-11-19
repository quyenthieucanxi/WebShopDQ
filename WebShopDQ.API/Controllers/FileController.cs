using Microsoft.AspNetCore.Mvc;
using WebShopDQ.App.Common;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;
using WebShopDQ.App.Services;
using WebShopDQ.App.Services.IServices;

namespace WebShopDQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileUploadService;
        private readonly ITokenInfoService _tokenInfoService;
        public FileController(IFileService fileUploadService, ITokenInfoService tokenInfoService)
        {
            _fileUploadService = fileUploadService;
            _tokenInfoService = tokenInfoService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromForm] FormFileDTO formFileDTO)
        {
            var infoToken = await _tokenInfoService.GetTokenInfo();
            var userId = infoToken.UserId;
            var file = await _fileUploadService.UploadFile(formFileDTO.FormFile, userId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Upload successfully.", Data = file });
        }
    }
}
