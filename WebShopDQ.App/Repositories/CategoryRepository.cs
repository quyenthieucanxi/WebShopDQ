using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Common;
using WebShopDQ.App.Common.Exceptions;
using WebShopDQ.App.Data;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DatabaseContext _databaseContext;
        public CategoryRepository(
            DatabaseContext databaseContext, 
            IMapper mapper, 
            IUnitOfWork unitOfWork) : base(databaseContext)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _databaseContext = databaseContext;
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

        public async Task<bool> Update(Guid catId, CategoryDTO categoryDTO)
        {
            var categoryExist = await GetById(catId) ??
                    throw new KeyNotFoundException(Messages.CategoryNotFound);
            var category = await FindAsync(c => c.CategoryPath == categoryDTO.CategoryPath);
            if (category is not null)
            {
                throw new DuplicateException("Tên đã tồn tại");
            }
            try
            {
                _unitOfWork.BeginTransaction();
                var entry = _databaseContext.Entry(categoryExist);
                entry.CurrentValues.SetValues(categoryDTO);
                await _unitOfWork.SaveChanges();
                _unitOfWork.CommitTransaction();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                throw new Exception(ex.Message);
            }
        }
    }
}
