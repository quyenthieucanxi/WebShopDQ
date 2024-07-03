using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Services.IServices
{
    public interface INotifyService
    {
        Task<int> CountNotifiesNotIsRead(Guid userId);
        Task<ICollection<NotifyViewModel>> GetByUser(Guid userId, int page, int pageSize, bool? status);
        Task NotifyFollowersAsync(Guid userIdSender, string userFullName, string avatarUrl, string productName);
        Task<bool> UpdateIsRead(Guid id, Guid userId);
    }
}
