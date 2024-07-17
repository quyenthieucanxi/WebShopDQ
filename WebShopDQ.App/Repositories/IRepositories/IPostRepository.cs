using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Repositories.IRepositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<PostListViewModel> GetAllByItemPage(int page, int limit,string? catName,string? search, string? orderByDirectionion);
        Task<PostListViewModel> GetAllTrend(int page, int limit);
        Task<PostListViewModel> GetByRequestTrendByUrl(int? page, int? limit, string status, Guid id);
        Task<PostListViewModel> GetByStatus(int? page, int? limit, string status, Guid userId);
        Task<bool> Update(UpdatePostDTO postDTO);
    }
}
