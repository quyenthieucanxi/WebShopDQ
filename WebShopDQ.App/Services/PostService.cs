using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Common;
using WebShopDQ.App.Common.Exceptions;
using WebShopDQ.App.Data;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.Services.IServices;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Services
{
    public class PostService : IPostService
    {
        private readonly IBackgroundJobClient _backgroundJobClient ;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;


        public PostService(
            IPostRepository postRepository,
            IUserRepository userRepository,
            UserManager<User> userManager,
            ICategoryRepository categoryRepository,
            IUnitOfWork uow,
            IMapper mapper,
            IBackgroundJobClient backgroundJobClient
,
            RoleManager<Role> roleManager)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _userManager = userManager;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _backgroundJobClient = backgroundJobClient;
            _roleManager = roleManager;
        }

        public async Task<bool> Create(PostDTO postDTO, Guid userId)
        {
            var postExist = await _postRepository.FindAsync(p => p.PostPath == postDTO.PostPath);
            if (postExist != null)
            {
                throw new DuplicateException("Tên đã tồn tại");
            }
            try
            {
                var data = await _userRepository.GetById(userId);
                var role = await _userManager.GetRolesAsync(data!);
                var post = new Post (Guid.NewGuid(),userId,postDTO.CategoryId,postDTO.Title!,
                    postDTO.PostPath!,postDTO.Description,postDTO.UrlImage,postDTO.Price,
                    postDTO.Address,postDTO.Quantity);
                if (role[0] == "Seller")
                {
                    post.Status = "Đang hiển thị";
                }
                await _postRepository.Add(post);
                if (role[0] == "Seller")
                {
                    _backgroundJobClient.Enqueue<NotifyService>(service => service.NotifyFollowersAsync(data!.Id,data!.FullName,data!.AvatarUrl,post.Title));
                }
                else
                {
                    _backgroundJobClient.Enqueue<NotifyService>(service => service.NotifyWhenUserCreatePost(data!.Id,data.FullName, data!.AvatarUrl,post.Title));
                }
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<PostViewModel>> GetAll()
        {
            var postList = new List<PostViewModel>();
            try
            {
                var data =  await _postRepository.FindAllAsync(p => p.Status == "Chờ duyệt");
                foreach (var item in data.OrderByDescending(p=> p.CreatedTime))
                {
                    var post = _mapper.Map<PostViewModel>(item);
                    postList.Add(post);
                }
                return postList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
                 
        }

        public async Task<PostListViewModel> GetAllByItemPage(int page, int limit,string? catName,string? search,string? orderByDirection)
        {
            return await _postRepository.GetAllByItemPage(page, limit,catName,search, orderByDirection);
        }

        public async Task<PostListViewModel> GetByStatus(int? page, int? limit, string status, Guid userId)
        {
            return await _postRepository.GetByStatus(page, limit, status, userId);
        }

        public async Task<PostViewModel> GetById(Guid postId)
        {
            try
            {
                var data = await _postRepository.FindAsync(p => p.Id == postId,new string []{"User"});
   
                var post = _mapper.Map<PostViewModel>(data);
                return post;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateStatus(Guid postId,string status)
        {
            var post = await _postRepository.GetById(postId) ?? throw new KeyNotFoundException(Messages.PostNotFound);
            post.Status = status;
            await _postRepository.Update(post);
            return await Task.FromResult(true);
        }

        public async Task<PostViewModel> GetByPath(string pathPost)
        {
            try
            {
                var data = await _postRepository.FindAsync(p => p.PostPath == pathPost, new string[] { "User" })
                        ?? throw new KeyNotFoundException(Messages.PostNotFound);
                var role = await _userManager.GetRolesAsync(data.User!);
                var post = _mapper.Map<PostViewModel>(data);
                post.User!.Role = role![0]; 
                return post;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> Update(UpdatePostDTO postDTO)
        {
            return _postRepository.Update(postDTO);
        }

        public async Task<PostListViewModel> GetByStatusByUrl(int? page, int? limit, string status, string url)
        {
            var user = await  _userRepository.FindAsync(u => u.Url == url) ?? throw new KeyNotFoundException(Messages.UserNotFound);
            return await _postRepository.GetByStatus(page, limit, status, user.Id);
        }
    }
}
