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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("[action]")]
        //[Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create(CategoryDTO categoryDTO)
        {
            await _categoryService.Create(categoryDTO);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Create category successfully." });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll(int page, int limit)
        {
            var categoryList = await _categoryService.GetAll(page, limit);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Get all category successfully.", Data = categoryList });
        }

        [HttpPut("[action]")]
        //[Authorize(Roles = "Manager")]
        public async Task<IActionResult> Update(Guid categoryId, CategoryDTO model)
        {
            var categoryUpdate = await _categoryService.Update(categoryId, model);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Update category successfully." });
        }
    }
}
