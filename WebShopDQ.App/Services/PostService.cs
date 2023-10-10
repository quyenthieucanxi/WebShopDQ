using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly DatabaseContext _databaseContext;
        private readonly IUnitOfWork _uow;
        private readonly ICategoryRepository _categoryRepository;
        

        public PostService(DatabaseContext databaseContext, IPostRepository postRepository, IUserRepository userRepository,
            ICategoryRepository categoryRepository, IUnitOfWork uow)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _databaseContext = databaseContext;
            _uow = uow;
        }

        public async Task<bool> Create(PostDTO postDTO, Guid userId)
        {
            var user = await _userRepository.GetById(userId);
            try
            {
                var post = new Post
                {
                    UserID = user!.Id,
                    CategoryID = postDTO.CategoryId,
                    Title = postDTO.Title,
                    Description = postDTO.Description,
                    UrlImage = postDTO.UrlImage,
                    Price = postDTO.Price,
                    Address = postDTO.Address,
                    Quantity = postDTO.Quantity
                };
                await _postRepository.Add(post);
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PostListViewModel> GetAll(int page, int limit)
        {
            return await _postRepository.GetAll(page, limit);
        }

        public async Task<PostListViewModel> GetByStatus(int page, int limit, string status, Guid userId)
        {
            return await _postRepository.GetByStatus(page, limit, status, userId);
        }

        public async Task<bool> UpdateStatus(Guid postId)
        {
            var post = await _postRepository.GetById(postId) ?? throw new KeyNotFoundException(Messages.PostNotFound);
            post.Status = "Đang hiển thị";
            await _postRepository.Update(post);
            return await Task.FromResult(true);
        }
    }
}
