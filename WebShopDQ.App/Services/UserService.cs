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
using WebShopDQ.App.Common.Exceptions;
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
        private readonly UserManager<User> _userManager;
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly ISavePostRepository _savePostRepository;
        private readonly IPostRepository _postRepository;
        private readonly IFileRepository _fileUploadRepository;
        private readonly IAddressShippingRepository _addressShippingRepository;
        private readonly IOrderRepository _orderRepository;

        public UserService(IUserRepository userRepository, IMapper mapper,
            IFriendshipRepository friendshipRepository, ISavePostRepository savePostRepository,
            IPostRepository postRepository, IFileRepository fileUploadRepository,IAddressShippingRepository addressShippingRepository,
            IOrderRepository orderRepository,UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _friendshipRepository = friendshipRepository;
            _savePostRepository = savePostRepository;
            _postRepository = postRepository;
            _fileUploadRepository = fileUploadRepository;
            _userManager = userManager;
           _addressShippingRepository = addressShippingRepository;
            _orderRepository = orderRepository;
        }
        public async Task<UserInfoViewModel> GetById(Guid userId)
        {
            try
            {
                var data = await _userRepository.GetById(userId);
                if (data == null)
                {
                    throw new KeyNotFoundException(Messages.UserNotFound);
                }
                var role =  await _userManager.GetRolesAsync(data);
                var followingCount = await _friendshipRepository.Count(f => f.FollowerID == userId);
                var followedCount = await _friendshipRepository.Count(f => f.FollowingID == userId);

                var user = _mapper.Map<UserInfoViewModel>(data);
                user.Role =  role![0];
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

        public async Task<bool> CheckUserByEmail(string email)
        {
            var user =  await _userRepository.CheckExist(p => p.Email == email);
            if (user != null)
            {
                return await Task.FromResult(true);
            }
            throw new KeyNotFoundException(Messages.UserNotFound);
        }

        public async Task<bool> AddLikePost(Guid userId, Guid postId)
        {
            var savePost = await _savePostRepository.FindAsync(p => p.UserID == userId && p.PostID == postId);
            if (savePost  != null)
            {
                throw new DuplicateException("Post is exist in List");
            }
            SavePosts savePosts = new SavePosts () { Id = Guid.NewGuid(), UserID = userId, PostID = postId };
            await _savePostRepository.Add(savePosts);
            return await Task.FromResult(true);
        }

        public async Task<PostListViewModel> GetSavesPost(Guid userId)
        {
            var data =  await _userRepository.FindAsync(p=> p.Id == userId,new string[] { "SavePosts.Post.Category", "SavePosts.Post.User" }) 
                                        ?? throw new KeyNotFoundException(Messages.UserNotFound);
            var savePosts = data.SavePosts ?? new List<SavePosts>();
            var totalCount = savePosts.Count ;
            var listSavePost = new List<PostViewModel>();
            foreach (var item in savePosts)
            {
                var post = _mapper.Map<PostViewModel>(item.Post);
                listSavePost.Add(post);
            }
            return new PostListViewModel
            {
                TotalPost = totalCount,
                PostList = listSavePost
            };
        }

        public async Task<bool> RemoveSavesPost(Guid userId, Guid postId)
        {
            await _savePostRepository.Remove(p => p.UserID == userId && p.PostID == postId);
            return await Task.FromResult(true);
        }

        public async Task<bool> CheckSavesPost(Guid userId, string pathPost)
        {
            var post = await _postRepository.FindAsync(p => p.PostPath == pathPost) ?? throw new KeyNotFoundException("Post in not found");
            var savePost = await _savePostRepository.FindAsync(p => p.UserID == userId && p.PostID == post.Id);
            if (savePost == null)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);  
        }
        public async Task<bool> CreateAddRessShipping(Guid userId, AddressShippingDTO addressShippingDTO)
        {
            var res = await  _addressShippingRepository.CreateAddress(userId, addressShippingDTO);
            return res ;
        }

        public async Task<AddressShippngListViewModel> GetAddressShopping(Guid userId)
        {
            var data = await _addressShippingRepository.FindAllAsync(p => p.UserId == userId);
            var totalCount = data.Count();
            var listAddressShipping = _mapper.Map<ICollection<AddressShippngViewModel>>(data);
            return new AddressShippngListViewModel
            {
                TotalPost = totalCount,
                listAddressShipping = listAddressShipping,
            };
        }

        public async Task<bool> RemoveAddressShopping(Guid userId, Guid addressShippingId)
        {
            try
            {
                await _addressShippingRepository.Remove(p => p.Id == addressShippingId && p.UserId == userId);
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);    
            }
        }

        public async Task<bool> UpdateAddressShopping(Guid userId, Guid addressShippingId, AddressShippingDTO addressShippingDTO)
        {
            var res = await _addressShippingRepository.UpdateAddress(userId,addressShippingId, addressShippingDTO);
            return res;
        }

        public async Task<AddressShippngViewModel> GetAddressShoppingDeFault(Guid userId)
        {
            var address = await _addressShippingRepository.GetDefault(userId) ?? throw new KeyNotFoundException(Messages.AddressShippingNotFound);
            var addressVM = _mapper.Map<AddressShippngViewModel>(address);
            return addressVM;
        }

        public async Task<bool> SetAddressShopping(Guid userId, Guid addressShippingId)
        {
            var res = await _addressShippingRepository.SetDefault(userId, addressShippingId);
            return res;
        }
    }
}
