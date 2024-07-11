using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.DTO;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Services.IServices
{
    public interface IChatService
    {
        Task<bool> Create(ChatDTO chatDTO);
        Task<ICollection<ChatViewModel>> GetChats(Guid userId,bool? status);
        Task<ICollection<GroupChatByTime>> GetHistoryChat(Guid senderId, string urlReceiver);
        Task<bool> UpdateIsRead(Guid id,Guid userId);
    }
}
