using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Services.IServices
{
    public interface IUserService
    {
        Task<UserInfoViewModel> GetById(Guid userId);
    }
}
