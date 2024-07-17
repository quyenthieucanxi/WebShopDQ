using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Services.IServices
{
    public interface IPostService
    {
        Task<bool> Create(PostDTO postDTO, Guid userId);
        Task<PostListViewModel> GetAllByItemPage(int page, int limit,string? catName,string? search,string? orderByDirection);
        Task<PostListViewModel> GetByStatus(int? page, int? limit, string status, Guid userId);
        Task<bool> UpdateStatus(Guid postId, string status);
        Task<PostViewModel> GetById(Guid postId);
        Task<IEnumerable<PostViewModel>> GetAll();
        Task<PostViewModel> GetByPath(string pathPost);
        Task<bool> Update(UpdatePostDTO postDTO);
        Task<PostListViewModel> GetByStatusByUrl(int? page, int? limit, string status, string url);
        Task<IEnumerable<PostViewModel>> GetAllRequestTrend();
        Task<bool> UpdateRequestTrend(Guid idPost, string status);
        Task<PostListViewModel> GetByRequestTrendByUrl(int? page, int? limit, string status, string url);
    }
}
