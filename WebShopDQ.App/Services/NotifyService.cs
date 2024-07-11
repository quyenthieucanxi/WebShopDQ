using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Hubs;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.Services.IServices;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Services
{
    public class NotifyService : INotifyService
    {
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly INotifyRepository _notifyRepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _hubContext;
        public NotifyService(
            IFriendshipRepository friendshipRepository,
            IHubContext<ChatHub> hubContext,
            INotifyRepository notifyRepository,
            IMapper mapper)
        {
            _friendshipRepository = friendshipRepository;
            _hubContext = hubContext;
            _notifyRepository = notifyRepository;
            _mapper = mapper;
        }

        public async Task<int> CountNotifiesNotIsRead(Guid userId)
        {
            var notifiesQuery = await _notifyRepository.FindAllAsync(n => n.UserIdReceiver == userId);
            return notifiesQuery.Count();
        }

        public async Task<ICollection<NotifyViewModel>> GetByUser(Guid userId, int page, int pageSize, bool? status)
        {
            string[] includes = new string[] {nameof(Notify.UserSender)};

            Task<IEnumerable<Notify>> notifiesQuery;
            if (status is not null)
            {
                notifiesQuery = _notifyRepository.FindAllAsync(n => n.UserIdReceiver == userId && n.IsRead == status, includes);
            }
            else
            {
                notifiesQuery = _notifyRepository.FindAllAsync(n => n.UserIdReceiver == userId, includes);
            }
            var notifies = await notifiesQuery;
            notifies = notifies.OrderByDescending(n => n.CreatedTime)
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize);
            var notifiesVM = _mapper.Map<ICollection<NotifyViewModel>>(notifies);
            return notifiesVM;

        }

        public async Task NotifyFollowersAsync(Guid userIdSender,string userFullName,string avatarUrl, string productName)
        {
            var followers = await _friendshipRepository.GetFollowers(userIdSender);
            foreach (var follower in followers)
            {
                
                await SendNotifyToFollowers(follower.FollowerID, userIdSender, userFullName, avatarUrl, productName);
            }
        }

        public async Task<bool> UpdateIsRead(Guid id, Guid userId)
        {
            var notify = await _notifyRepository.GetById(id);
            notify.IsRead = true;
            await _notifyRepository.Update(notify);
            return await Task.FromResult(true);
        }

        private async Task SendNotifyToFollowers(Guid userIdReceiver, Guid userIdSender, string name, string avatarUrl, string productName)
        {
            string notifyText = $"{name} vừa đăng tin: {productName}";
            var notifyDTO = new NotifyDTO {UserIdSender = userIdSender ,UserIdReceiver = userIdReceiver, IsRead = false, NotifyText = notifyText, };
            var notifyTemp = new
            {
                NotifyText = notifyText,
                AvatarUrl = avatarUrl
            };

            await Task.WhenAll(_hubContext.Clients.Group(userIdReceiver.ToString()).SendAsync("ReceiveNotification", notifyTemp),
                    _notifyRepository.Create(notifyDTO));
           
        }
    }
}
