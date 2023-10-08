using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Common;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.Services.IServices;

namespace WebShopDQ.App.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly IUserRepository _userRepository;

        public FriendshipService(IFriendshipRepository friendshipRepository, IUserRepository userRepository)
        {
            _friendshipRepository = friendshipRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> Follow(Guid followerId, Guid followingId)
        {
            var follower = await _userRepository.GetById(followerId);
            var following = await _userRepository.GetById(followingId) ?? throw new KeyNotFoundException(Messages.UserNotFound);
            var friendship = new Friendship
            {
                FollowerID = follower!.Id,
                FollowingID = following!.Id,
            };
            await _friendshipRepository.Add(friendship);
            return await Task.FromResult(true);
        }

        public async Task<bool> UnFollow(Guid followerId ,Guid followingId)
        {
            var user = await _friendshipRepository.GetById(followingId) ?? throw new KeyNotFoundException(Messages.UserNotFound);
            var result = await _friendshipRepository
                .Remove(f => f.FollowerID == followerId && f.FollowingID == followingId);
            return result;
        }
    }
}
