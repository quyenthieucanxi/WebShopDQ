using Microsoft.AspNetCore.Mvc;
using WebShopDQ.App.Common;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;
using WebShopDQ.App.Services.IServices;

namespace WebShopDQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileUploadService;
        public FileController(IFileService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromForm] FormFileDTO formFileDTO)
        {
            var file = await _fileUploadService.UploadFile(formFileDTO.FormFile);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Upload successfully.", Data = file });
        }
    }
}
