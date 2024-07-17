using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebShopDQ.App.Common;
using WebShopDQ.App.Common.Constant;
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
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IHubContext<ChatHub> _hubContext;
        public NotifyService(
            IFriendshipRepository friendshipRepository,
            IHubContext<ChatHub> hubContext,
            INotifyRepository notifyRepository,
            IMapper mapper,
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            _friendshipRepository = friendshipRepository;
            _hubContext = hubContext;
            _notifyRepository = notifyRepository;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
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
            string titleNotify = "ReceiveNotification";
            string notifyText = $"{userFullName} vừa đăng tin: {productName}";
            var followers = await _friendshipRepository.GetFollowers(userIdSender);
            foreach (var follower in followers)
            {
                
                await SendNotify(follower.FollowerID, userIdSender, avatarUrl, titleNotify,notifyText);
            }
        }

        public async Task NotifyWhenUserCreatePost(Guid userIdSender,
            string userFullName, string avatarUrl, string productName)
        {
            
            var roleAdmin = await _roleManager.FindByNameAsync(RoleConstant.Admin)
                ?? throw new KeyNotFoundException("Role không tồn tại");
            string titleNotify = "ReceiveNotificationCreatePost";
            string notifyText = $"{userFullName} vừa đăng tin: {productName}";
            foreach (var user in _userManager.Users.ToList())
            {
                if (await _userManager.IsInRoleAsync(user,RoleConstant.Admin))
                {
                    await SendNotify(user.Id, userIdSender, avatarUrl, titleNotify,notifyText);
                }
            }
        }
        public async Task NotifyWhenUserCreateOrder(Guid userIdSender,Guid userIdReceiver,string productName)
        {
            var userSender = await _userManager.FindByIdAsync(userIdSender.ToString())
                ?? throw new KeyNotFoundException(Messages.UserNotFound);
            string titleNotify = "ReceiveNotificationCreateOrder";
            string notifyText = $"{userSender.FullName} vừa đặt hàng sản phẩm: {productName}";
            await SendNotify(userIdReceiver, userIdSender, userSender!.AvatarUrl, titleNotify, notifyText);
        }
        public async Task NotifyWhenUserCreateShop(Guid userIdSender,
            string userFullName, string avatarUrl, string shopName)
        {
            var roleAdmin = await _roleManager.FindByNameAsync(RoleConstant.Admin)
                ?? throw new KeyNotFoundException("Role không tồn tại");
            string titleNotify = "ReceiveNotificationCreateShop";
            string notifyText = $"{userFullName} vừa tạo cửa hàng : {shopName}";
            foreach (var user in _userManager.Users.ToList())
            {
                if (await _userManager.IsInRoleAsync(user, RoleConstant.Admin))
                {
                    await SendNotify(user.Id, userIdSender, avatarUrl, titleNotify, notifyText);
                }
            }
        }
        public async Task NotifyWhenSellerUpdateStatusOrder(Guid userIdSender, Guid userIdReceiver,
                     Guid IdShop ,string postName,string nameSeller,
                            string avartarSeller,string status)
        {
            var userSender = await _userManager.FindByIdAsync(userIdSender.ToString())
                ?? throw new KeyNotFoundException(Messages.UserNotFound);
            string titleNotify = "ReceiveNotificationUpdateStatusOrder";
            string notifyText = string.Empty;
            if (await _userManager.IsInRoleAsync(userSender, RoleConstant.Seller))
            {
                notifyText = $"{nameSeller} vừa cập nhập trạng thái đơn hàng : {postName} thành {status}";
                await SendNotify(userIdReceiver, userIdSender, userSender!.AvatarUrl, titleNotify, notifyText);
            }
            else
            {
                notifyText = $"{userSender.FullName} vừa cập nhập trạng thái đơn hàng : {postName} thành {status}";
                await SendNotify(IdShop, userIdSender, avartarSeller, titleNotify, notifyText);
            }
            
        }
        public async Task NotifyWhenUpdateStatusPost(Guid userIdSender, Guid userIdReceiver, string productName,string status)
        {
            
            var userReceiver = await _userManager.FindByIdAsync(userIdReceiver.ToString())
                ?? throw new KeyNotFoundException(Messages.UserNotFound);
            string titleNotify = "ReceiveNotificationUpdateStatusPost";
            string notifyText = "";
            if (status == ShopStatus.View)
            {
                notifyText = $"Tin {productName} của bạn đã được duyệt ";
            }
            else
            {
                notifyText = $"Tin {productName} của bạn đã không được duyệt ";
            }
            await SendNotify(userIdReceiver, userIdSender, userReceiver!.AvatarUrl, titleNotify, notifyText);
        }
        public async Task NotifyWhenUpdateRequestTrend(Guid userIdSender, Guid userIdReceiver, string productName, string status)
        {
            var userSender = await _userManager.FindByIdAsync(userIdSender.ToString())
                ?? throw new KeyNotFoundException(Messages.UserNotFound);
            var roleAdmin = await _roleManager.FindByNameAsync(RoleConstant.Admin)
                ?? throw new KeyNotFoundException("Role không tồn tại");
            string titleNotify = "ReceiveNotificationCreateRequestTrend";
            string notifyText = string.Empty; 
            if (await _userManager.IsInRoleAsync(userSender, RoleConstant.Admin))
            {
                notifyText = $"{userSender.FullName} vừa yêu cầu tin: {productName} lên nổi bật";
                await SendNotify(userIdReceiver, userSender.Id, userSender.AvatarUrl, titleNotify, notifyText);
            }
            else
            {
                foreach (var user in _userManager.Users.ToList())
                {
                    if (await _userManager.IsInRoleAsync(user, RoleConstant.Admin))
                    {
                        notifyText = $"Yêu cầu tin: {productName} của bạn đã được  ${status} ";
                        await SendNotify(user.Id, userIdSender, userSender.AvatarUrl, titleNotify, notifyText);
                    }
                }
            } 
            
            
        }
        public async Task NotifyWhenUpdateStatusShop(Guid userIdSender, Guid userIdReceiver,string AvatarUrl, string status,string shopName )
        {
  
            string titleNotify = "ReceiveNotificationUpdateStatusShop";
            string notifyText =  "";
            if (status == ShopStatus.View)
            {
                notifyText = $"Cửa hàng {shopName} của bạn đã được duyệt ";
            }
            else
            {
                notifyText = $"Cửa hàng {shopName} của bạn đã không được duyệt ";
            }
            await SendNotify(userIdReceiver, userIdSender, AvatarUrl, titleNotify, notifyText);
        }
        public async Task<bool> UpdateIsRead(Guid id, Guid userId)
        {
            var notify = await _notifyRepository.GetById(id);
            notify.IsRead = true;
            await _notifyRepository.Update(notify);
            return await Task.FromResult(true);
        }

        private async Task SendNotify(Guid userIdReceiver, Guid userIdSender, string avatarUrl,string titleNotify, string notifyText)
        {
            var notifyDTO = new NotifyDTO {UserIdSender = userIdSender ,UserIdReceiver = userIdReceiver, IsRead = false, NotifyText = notifyText, };
            var notifyTemp = new
            {
                NotifyText = notifyText,
                AvatarUrl = avatarUrl
            };

            await Task.WhenAll(_hubContext.Clients.Group(userIdReceiver.ToString()).SendAsync(titleNotify, notifyTemp),
                    _notifyRepository.Create(notifyDTO));
           
        }

        
    }
}
