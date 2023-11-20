using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.DTO;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Services.IServices
{
    public interface ICategoryService
    {
        Task<bool> Create(CategoryDTO categoryDTO);
        Task<IEnumerable<CategoryViewModel>> GetAll();
        Task<CategoryListViewModel> GetAllByPageNumber(int page, int limit);
        Task<bool> Update(Guid idCategory, CategoryDTO categoryDTO);
        Task<bool> Delete(Guid idCategory);
    }
}
