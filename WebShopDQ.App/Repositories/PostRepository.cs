using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebShopDQ.App.Data;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly IMapper _mapper;

        public PostRepository(IMapper mapper, DatabaseContext databaseContext) : base(databaseContext)
        {
            _mapper = mapper;
        }

        public async Task<PostListViewModel> GetAllByItemPage(int page, int limit)
        {
            try
            {
                var query = Entities.Include(p => p.Category)
                                    .Include(p => p.User);
                page = page != 0 ? page : 1;
                limit = limit != 0 ? limit : 10;
                var listData = new List<PostViewModel>();
                var data = await query.OrderByDescending(post => post.CreatedTime).Where(post => post.Status == "Đang hiển thị").ToListAsync();
                var totalCount = data.Count;
                data = data.Skip((page - 1) * limit).Take(limit).ToList();
                foreach (var item in data)
                {
                    var post = _mapper.Map<PostViewModel>(item);
                    listData.Add(post);
                }
                return new PostListViewModel
                {
                    TotalPost = totalCount,
                    PostList = listData
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PostListViewModel> GetByStatus(int page, int limit, string status, Guid userId)
        {
            try
            {
                var query = Entities.Include(p => p.Category)
                                    .Include(p => p.User);
                page = page != 0 ? page : 1;
                limit = limit != 0 ? limit : 4;
                var listData = new List<PostViewModel>();
                var data = await query.OrderByDescending(post => post.CreatedTime)
                    .Where(p => p.Status == status && p.UserID == userId).ToListAsync();
                var totalCount = data.Count;
                data = data.Skip((page - 1) * limit).Take(limit).ToList();
                foreach (var item in data)
                {
                    var post = _mapper.Map<PostViewModel>(item);
                    listData.Add(post);
                }
                return new PostListViewModel
                {
                    TotalPost = totalCount,
                    PostList = listData
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
