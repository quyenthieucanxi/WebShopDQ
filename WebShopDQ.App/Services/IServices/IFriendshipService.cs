using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.Services.IServices
{
    public interface IFriendshipService
    {
        Task<bool> Follow(Guid followerId, Guid followingId);
        Task<bool> UnFollow(Guid followerId, Guid followingId);
    }
}
