using AutoMapper;
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
using WebShopDQ.App.Repositories;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.Services.IServices;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _uow;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        

        public PostService(IPostRepository postRepository, IUserRepository userRepository,
            ICategoryRepository categoryRepository, IUnitOfWork uow, IMapper mapper)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _uow = uow;
            _mapper = mapper;
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

        public async Task<PostViewModel> GetById(Guid postId)
        {
            try
            {
                var data = await _postRepository.GetById(postId);

                var post = _mapper.Map<PostViewModel>(data);
                return post;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
