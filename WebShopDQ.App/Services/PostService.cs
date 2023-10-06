using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Common;
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
        private readonly ICategoryRepository _categoryRepository;
        

        public PostService(IPostRepository postRepository, IUserRepository userRepository,
            ICategoryRepository categoryRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> Create(PostDTO postDTO, Guid userId, Guid categoryId)
        {
            var user = await _userRepository.GetById(userId);
            var category = await _categoryRepository.GetById(categoryId) ?? 
                throw new KeyNotFoundException(Messages.CategoryNotFound);
            try
            {
                var post = new Post
                {
                    UserID = user!.Id,
                    CategoryID = category!.Id,
                    Title = postDTO.Title,
                    Description = postDTO.Description,
                    UrlImage = postDTO.UrlImage,
                    Price = postDTO.Price,
                    Address = postDTO.Address,
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
    }
}
