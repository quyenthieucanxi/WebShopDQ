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
        [HttpGet("{catId}")]
        public async Task<IActionResult> GetById(Guid catId)
        {
            var category = await _categoryService.GetById(catId);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Get detail category successfully.", Data = category });
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllByPageNumber(int page, int limit)
        {
            var categoryList = await _categoryService.GetAllByPageNumber(page, limit);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Get all category successfully.", Data = categoryList });
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var categoryList = await _categoryService.GetAll();
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Get all category successfully.", Data = categoryList });
        }

        [HttpPut("[action]/{catId}")]
        //[Authorize(Roles = "Manager")]
        public async Task<IActionResult> Update(Guid catId, CategoryDTO model)
        {
            var categoryUpdate = await _categoryService.Update(catId, model);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Update category successfully." });
        }

        [HttpDelete("[action]/{idCategory}")]
        //[Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(Guid idCategory)
        {
            var userDelete = await _categoryService.Delete(idCategory);
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Code = 200, Message = "Delete category successfully." });
        }
    }
}
