using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.DTO;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Services.IServices
{
    public interface IPostService
    {
        Task<bool> Create(PostDTO postDTO, Guid userId);
        Task<PostListViewModel> GetAll(int page, int limit);
        Task<PostListViewModel> GetByStatus(int page, int limit, string status, Guid userId);
        Task<bool> UpdateStatus(Guid postId);
        Task<PostViewModel> GetById(Guid postId);
    }
}
