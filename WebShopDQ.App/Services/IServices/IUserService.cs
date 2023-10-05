using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.DTO;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Services.IServices
{
    public interface IUserService
    {
        Task<UserInfoViewModel> GetById(Guid userId);
        Task<bool> Update(Guid userId, UserInfoDTO model);
        Task<UserListViewModel> GetAll(int page, int limit);
        Task<bool> Delete(Guid id);
    }
}
