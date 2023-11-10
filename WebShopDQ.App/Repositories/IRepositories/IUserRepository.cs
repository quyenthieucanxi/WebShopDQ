using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Repositories.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<UserListViewModel> GetAll(int page, int limit);
        Task<bool> Update(Guid id, UserInfoDTO model);
    }
}
