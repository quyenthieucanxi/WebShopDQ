using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Common;
using WebShopDQ.App.Common.Exceptions;
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

        public async Task<bool> CheckFollow(Guid userId, string url)
        {
            var userFollower = await _userRepository.FindAsync(u => u.Url == url) ?? throw new KeyNotFoundException(Messages.UserNotFound);
            var existingFriendship = await _friendshipRepository.FindAsync(f => f.FollowingID == userFollower.Id && f.FollowerID == userId );
            return existingFriendship != null;
        }

        public async Task<int> CountFollower(string url)
        {
            var user = await _userRepository.FindAsync(u => u.Url == url) ?? throw new KeyNotFoundException(Messages.UserNotFound);
            var listFollower = await _friendshipRepository.FindAllAsync(f => f.FollowingID == user.Id);
            return listFollower.Count() ;
        }

        public async Task<int> CountFollowing( string url)
        {
            var user = await _userRepository.FindAsync(u => u.Url == url) ?? throw new KeyNotFoundException(Messages.UserNotFound);
            var listFollowing = await _friendshipRepository.FindAllAsync(f => f.FollowerID == user.Id);
            return listFollowing.Count();
        }

        public async Task<bool> Follow(Guid followerId, string url)
        {
            var following = await _userRepository.FindAsync(u => u.Url == url) ?? throw new KeyNotFoundException(Messages.UserNotFound);
            if (followerId == following.Id) throw new DuplicateException("Can not follow!");
            var existingFriendship = await _friendshipRepository.FindAsync(f => f.FollowerID == followerId && f.FollowingID == following.Id);
            if (existingFriendship != null)
            {
                throw new DuplicateException("Friendship already exists!");
            }

            var friendship = new Friendship(Guid.NewGuid(),followerId, following.Id);
            await _friendshipRepository.Add(friendship);
            return await Task.FromResult(true);
        }

        public async Task<bool> UnFollow(Guid followerId ,string url)
        {
            var following = await _userRepository.FindAsync(u => u.Url == url) ?? throw new KeyNotFoundException(Messages.UserNotFound);
            return await _friendshipRepository
                .Remove(f => f.FollowerID == followerId && f.FollowingID == following.Id);
        }
    }
}
