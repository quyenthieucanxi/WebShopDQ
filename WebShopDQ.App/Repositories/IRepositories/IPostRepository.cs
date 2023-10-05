using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Models;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Repositories.IRepositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<PostListViewModel> GetAll(int page, int limit);
    }
}
