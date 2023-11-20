using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Data;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly IMapper _mapper;
        public CategoryRepository(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext)
        {
            _mapper = mapper;
        }

        public async Task<CategoryListViewModel> GetAllByPageNumber(int page, int limit)
        {
            try
            {
                page = page != 0 ? page : 1;
                limit = limit != 0 ? limit : 10;
                var listData = new List<CategoryViewModel>();
                var data = await Entities.OrderByDescending(category => category.CreatedTime).ToListAsync();
                var totalCount = data.Count;
                data = data.Skip((page - 1) * limit).Take(limit).ToList();
                foreach (var item in data)
                {
                    var post = _mapper.Map<CategoryViewModel>(item);
                    listData.Add(post);
                }
                return new CategoryListViewModel
                {
                    TotalCategory = totalCount,
                    CategoryList = listData
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
