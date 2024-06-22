using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Common;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.Services.IServices;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ChatService(
            IChatRepository chatRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _chatRepository = chatRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> Create(ChatDTO chatDTO)
        {
            var sender = await _userRepository.FindAsync(u => u.Url == chatDTO.urlSender)
                                   ?? throw new KeyNotFoundException(Messages.UserNotFound);
            var receiver = await _userRepository.FindAsync(u => u.Url == chatDTO.urlReceiver)
                                   ?? throw new KeyNotFoundException(Messages.UserNotFound);
            var chat = new Chats(Guid.NewGuid(), sender.Id, receiver.Id,chatDTO.message,true,false);
            await _chatRepository.Add(chat);
            return await Task.FromResult(true);
        }

        public async Task<ICollection<ChatViewModel>> GetChats(Guid userId, bool? status)
        {
            var chats = await _chatRepository.GetChats(userId);
            if ( status is not null)
            {
                chats = chats.Where(c => c.isReceiverRead == status && c.ReceiverID == userId).ToList();
            }
            var chatsVM = _mapper.Map<ICollection<ChatViewModel>>(chats);
            return await Task.FromResult(chatsVM);
        }

        public async Task<ICollection<GroupChatByTime>> GetHistoryChat(Guid senderId, string urlReceiver)
        {
            var receiver = await _userRepository.FindAsync(u => u.Url == urlReceiver)
                                   ?? throw new KeyNotFoundException(Messages.UserNotFound);
            var chats = await _chatRepository.GetHistoryChat(senderId, receiver.Id);
            var groupChatsByTime = chats.GroupBy(chat => chat.CreatedTime.ToString("dd/MM/yyyy"))
                                   .Select(g => new GroupChatByTime
                                   {
                                       time = g.Key,
                                       messages = _mapper.Map<ICollection<ChatViewModel>>(g.ToList())
                                   }).ToList() ;
            return await Task.FromResult(groupChatsByTime);
        }

        public async Task<bool> UpdateIsRead(Guid id,Guid userId)
        {
            var chat = await _chatRepository.GetById(id);
            chat.isReceiverRead = true;
            await _chatRepository.Update(chat);
            return await Task.FromResult(true);
        }
    }
}
