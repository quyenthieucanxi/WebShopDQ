using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.Services.IServices;

namespace WebShopDQ.App.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IDecodeRepository _decodeRepository ;
        private readonly IUserRepository _userRepository;    
        private readonly IChatService _chatService;
        public ChatHub(IChatService chatService,
                    IDecodeRepository decodeRepository,
                    IUserRepository userRepository)
        {
            _chatService = chatService;
            _decodeRepository = decodeRepository;
            _userRepository = userRepository;
        }
        public async Task SendMessage(ChatDTO chatDTO)
        {
            var receiver = await _userRepository.FindAsync(u => u.Url == chatDTO.urlReceiver);
            await _chatService.Create(chatDTO);
            await Clients.Group(receiver.Id.ToString()).SendAsync("ReceiveMessage", chatDTO.urlSender, chatDTO.message);
        }
        public  async Task SendNotifyToFollowers(Guid userId,string name, string productName)
        {
            await Clients.Group(userId.ToString()).
                    SendAsync("ReceiveNotification", $"{name} vừa đăng tin: {productName}");
        }

        public override async Task OnConnectedAsync()
        {
            var token = Context.GetHttpContext()!.Request.Query["access_token"];
            var info =  _decodeRepository.Decode(token);
            await Groups.AddToGroupAsync(Context.ConnectionId, info.UserId);

            await base.OnConnectedAsync();
        }

    }
}
