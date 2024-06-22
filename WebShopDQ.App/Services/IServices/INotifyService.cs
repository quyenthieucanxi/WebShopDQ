using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories.IRepositories;

namespace WebShopDQ.App.Services.IServices
{
    public interface INotifyService
    {
        Task NotifyFollowersAsync(User user, string productName);

    }
}
