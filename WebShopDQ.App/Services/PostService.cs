using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
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

        public PostService(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> Create(PostDTO postDTO, Guid id)
        {
            var user = await _userRepository.GetById(id);
            try
            {
                var post = new Post
                {
                    UserID = user!.Id,
                    //UserID = new Guid(user.Id.ToString()),
                    CategoryID = postDTO.CategoryID,
                    Title = postDTO.Title,
                    Description = postDTO.Description,
                    UrlImage = postDTO.UrlImage,
                    Price = postDTO.Price,
                    Address = postDTO.Address,
                };
                var result = await _postRepository.Add(post);
                return result;
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
