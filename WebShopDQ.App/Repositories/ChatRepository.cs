using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Common.Constant;
using WebShopDQ.App.Data;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories.IRepositories;

namespace WebShopDQ.App.Repositories
{
    public class ChatRepository : Repository<Chats>, IChatRepository
    {
        public ChatRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<ICollection<Chats>> GetChats(Guid senderId)
        {
            var chats = await Entities
                        .Include(c => c.Receiver)
                        .Include(c => c.Sender)
                        .Where(chat => chat.SenderID == senderId || chat.ReceiverID == senderId)
                        .ToListAsync();

              chats = chats
                        .AsEnumerable()
                        .GroupBy(chat => GetPartnerId(chat, senderId))
                        .Select(group => group.OrderByDescending(chat => chat.CreatedTime).FirstOrDefault())
                        .OrderByDescending(chat => chat.CreatedTime).ToList();



            //var chatsByReceiver = await Entities.Include(c => c.Receiver)
            //                           .Include(c => c.Sender)
            //                           .Where(chat => chat.ReceiverID == senderId)
            //                           .GroupBy(chat => chat.SenderID)
            //                           .Select(group => group.OrderByDescending(chat => chat.CreatedTime).FirstOrDefault())
            //                           .ToListAsync();
            //var chats = new List<Chats?>();
            //if (chatsBySender.Count() > 0 && chatsByReceiver.Count() > 0)
            //{
            //    chats = chatsBySender.Concat(chatsByReceiver).OrderByDescending(c => c.CreatedTime).ToList();
            //}
            //else if (chatsBySender.Count() > 0)
            //{
            //    chats = chatsBySender;
            //}
            //else
            //{
            //    chats = chatsByReceiver; 
            //}
            return await Task.FromResult(chats);
        }

        public async Task<ICollection<Chats>> GetHistoryChat(Guid senderId, Guid receiverId)
        {
            var chats = await FindAllAsync(chat => chat.SenderID == senderId
                            && chat.ReceiverID == receiverId || chat.ReceiverID == senderId
                                && chat.SenderID == receiverId, null, null, chat => chat.CreatedTime);
            //if (chats.Count() == 0)
            //{
            //    chats = await FindAllAsync(chat => chat.ReceiverID == senderId
            //                    && chat.SenderID == receiverId, null, null, chat => chat.CreatedTime);
            //}
            return await Task.FromResult(chats.ToList());
        }
        private Guid GetPartnerId(Chats chat, Guid userId)
        {
            if (chat.SenderID == userId)
            {
                return chat.ReceiverID;
            }
            else
            {
                return chat.SenderID;
            }
        }
    }
}
