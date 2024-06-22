using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.Services.IServices
{
    public interface IFriendshipService
    {
        Task<bool> CheckFollow(Guid userId, string url);
        Task<int> CountFollower(string url);
        Task<int> CountFollowing(string url);
        Task<bool> Follow(Guid followerId, string url);
        Task<bool> UnFollow(Guid followerId, string url);
    }
}
