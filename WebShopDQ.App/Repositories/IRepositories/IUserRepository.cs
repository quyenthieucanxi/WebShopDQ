using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<UserInfoViewModel> GetById(Guid id);
    }
}
