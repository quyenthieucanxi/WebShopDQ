using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Models;

namespace WebShopDQ.App.Repositories.IRepositories
{
    public interface IChatRepository : IRepository<Chats>
    {
        Task<ICollection<Chats>> GetChats(Guid senderId);
        Task<ICollection<Chats>> GetHistoryChat(Guid senderId, Guid receiverId);
    }
}
