using AutoMapper;
using MailKit.Search;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Common;
using WebShopDQ.App.Data;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.Services.IServices;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly DatabaseContext _dbContext;
        private readonly IFriendshipRepository _friendshipRepository;

        public UserService(IUserRepository userRepository, IMapper mapper,
            DatabaseContext dbContext, IFriendshipRepository friendshipRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _dbContext = dbContext;
            _friendshipRepository = friendshipRepository;
        }

        public async Task<UserInfoViewModel> GetById(Guid userId)
        {
            try
            {
                var data = await _userRepository.GetById(userId);
                var followingCount = await _friendshipRepository.Count(f => f.FollowerID == userId);
                var followedCount = await _friendshipRepository.Count(f => f.FollowingID == userId);

                var user = _mapper.Map<UserInfoViewModel>(data);
                user.FollowedCount = followedCount;
                user.FollowingCount = followingCount;
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(Guid userId, UserInfoDTO model)
        {
            var data = await _userRepository.Update(userId, model);
            return data;
        }

        public async Task<UserListViewModel> GetAll(int page, int limit)
        {
            var data = await _userRepository.GetAll(page, limit);
            return data;
        }

        public async Task<bool> Delete(Guid userId)
        {
            var user = await _userRepository.GetById(userId);
            if (user != null)
            {
                user!.IsActive = false;
                await _userRepository.Update(user);
                return await Task.FromResult(true);
            }
            throw new KeyNotFoundException(Messages.UserNotFound);
        }
    }
}
