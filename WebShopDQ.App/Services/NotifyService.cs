using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Hubs;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.Services.IServices;

namespace WebShopDQ.App.Services
{
    public class NotifyService : INotifyService
    {
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly INotifyRepository _notifyRepository;
        private readonly IHubContext<ChatHub> _hubContext;
        public NotifyService(
            IFriendshipRepository friendshipRepository,
            IHubContext<ChatHub> hubContext,
            INotifyRepository notifyRepository)
        {
            _friendshipRepository = friendshipRepository;
            _hubContext = hubContext;
            _notifyRepository = notifyRepository;
        }

        public async Task NotifyFollowersAsync(User user,string productName)
        {
            var followers = await _friendshipRepository.GetFollowers(user.Id);
            foreach (var follower in followers)
            {
                
                await SendNotifyToFollowers(follower.Id, user!.FullName, productName);
            }
        }
        private async Task SendNotifyToFollowers(Guid userId, string name, string productName)
        {
            string notifyText = $"{name} vừa đăng tin: {productName}";
            var notifyDTO = new NotifyDTO { UserID = userId, IsRead = false, NotifyText = notifyText, };
            await Task.WhenAll(_hubContext.Clients.Group(userId.ToString()).SendAsync("ReceiveNotification", notifyText),
                    _notifyRepository.Create(notifyDTO));
           
        }
    }
}
