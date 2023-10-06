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
        private readonly DatabaseContext _databaseContext;

        public PostRepository(IMapper mapper, DatabaseContext databaseContext) : base(databaseContext)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public async Task<PostListViewModel> GetAll(int page, int limit)
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
    }
}
