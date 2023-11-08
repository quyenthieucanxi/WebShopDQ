using AutoMapper;
using MailKit.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly IFileRepository _fileUploadRepository;

        public UserService(IUserRepository userRepository, IMapper mapper,
            IFriendshipRepository friendshipRepository, IFileRepository fileUploadRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _friendshipRepository = friendshipRepository;
            _fileUploadRepository = fileUploadRepository;
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
            //var file = await _fileUploadRepository.UploadFile(formFile);
            //var userInfo = _mapper.Map<UserDTO>(model);
            //userInfo.UrlAvatar = file.Url;
            //userInfo.PublicIdAvatar = file.PublicId;
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
