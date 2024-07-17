using AutoMapper;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebShopDQ.App.Common;
using WebShopDQ.App.Data;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.Utils;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly UserManager<User> _userManager;
        private readonly DatabaseContext _databaseContext;

        public PostRepository(IMapper mapper,IUnitOfWork uow, UserManager<User> userManager, DatabaseContext databaseContext) : base(databaseContext)
        {
            _mapper = mapper;
            _uow = uow;
            _userManager = userManager;
            _databaseContext = databaseContext;
        }

        public async Task<PostListViewModel> GetAllByItemPage(int page, int limit,string? catName,string? search, string? orderByDirection)
         {
            try
            {
                var query = string.IsNullOrEmpty(catName)  ?
                            Entities.Include(p => p.Category)
                                    .Include(p => p.User).Where(post => post.Status == "Đang hiển thị") : 
                            Entities.Include(p => p.Category)
                                    .Include(p => p.User).Where(p => p.Category!.CategoryPath == catName 
                                                                && p.Status == "Đang hiển thị" );
                query = !string.IsNullOrEmpty(search) ? query.Where(p => p.PostPath!.ToLower().Contains(search.ToLower()) || p.Title.ToLower().Contains(search.ToLower()) ) : query;
                page = page != 0 ? page : 1;
                limit = limit != 0 ? limit : 10;
                var listData = new List<PostViewModel>();
                //var data = await query.OrderByDescending(post => post.CreatedTime).Where(post => post.Status == "Đang hiển thị").ToListAsync();
                var data = new List<Post> ();
                if (orderByDirection == "ASC")
                {
                    data = await query.OrderBy(p => p.Price).ThenByDescending(post => post.CreatedTime).ToListAsync();
                }   
                else if (orderByDirection == "DESC") {
                    data = await query.OrderByDescending(p => p.Price).ThenByDescending(post => post.CreatedTime).ToListAsync();
                }
                else
                {
                    data = await query.OrderByDescending(post => post.CreatedTime).ToListAsync();
                }
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

        public async Task<PostListViewModel> GetAllTrend(int page, int limit)
        {
            try
            {
                var query = Entities.Include(p => p.Category)
                                    .Include(p => p.User).Where(p => p.IsTrend == true && p.Status == "Đang hiển thị");
  
                page = page != 0 ? page : 1;
                limit = limit != 0 ? limit : 10;
                var listData = new List<PostViewModel>();
                var data = new List<Post>();
                data = await query.OrderByDescending(post => post.CreatedTime).ToListAsync();
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

        public async Task<PostListViewModel> GetByRequestTrendByUrl(int? page, int? limit, string status, Guid userId)
        {
            try
            {
                var query = Entities.Include(p => p.Category)
                                    .Include(p => p.User);
                var listData = new List<PostViewModel>();
                var data = await query.OrderByDescending(post => post.CreatedTime)
                    .Where(p => p.requestTrend!= null && p.requestTrend.ToLower() == status.ToLower() && p.UserID == userId).ToListAsync();
                var totalCount = data.Count;
                if (page != null && limit != null)
                {
                    page = page > 0 ? page : 1;
                    limit = limit > 0 ? limit : 4;
                    data = data.Skip((int)((page - 1) * limit)).Take((int)(limit)).ToList();
                }

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

        public async Task<PostListViewModel> GetByStatus(int? page, int? limit, string status, Guid userId)
        {
            try
            {
                var query = Entities.Include(p => p.Category)
                                    .Include(p => p.User);
                //page = page != 0 ? page : 1;
                //limit = limit != 0 ? limit : 4;
                var listData = new List<PostViewModel>();
                var data = await query.OrderByDescending(post => post.CreatedTime)
                    .Where(p => p.Status.ToLower() == status.ToLower() && p.UserID == userId).ToListAsync();
                var totalCount = data.Count;
                if (page != null && limit != null)
                {
                    page = page > 0 ? page : 1;
                    limit = limit > 0 ? limit : 4;
                    data = data.Skip((int)((page - 1) * limit)).Take((int)(limit)).ToList();
                }
                
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

        public async Task<bool> Update(UpdatePostDTO postDTO)
        {
            var data = await GetById(postDTO.Id) ?? throw new KeyNotFoundException(Messages.PostNotFound);
            try
            {
                _uow.BeginTransaction();
                var entry = _databaseContext.Entry(data);
                entry.CurrentValues.SetValues(postDTO);
                await _uow.SaveChanges();
                _uow.CommitTransaction();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _uow.RollbackTransaction();
                throw new Exception(ex.Message);
            }
        }
    }
}
