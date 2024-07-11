using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Common;
using WebShopDQ.App.Common.Exceptions;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.Services.IServices;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<bool> Create(CategoryDTO categoryDTO)
        {
            var categoryExist = await _categoryRepository.FindAsync(c => c.CategoryPath == categoryDTO.CategoryPath);
            if (categoryExist is not null)
            {
                throw new DuplicateException("Tên đã tồn tại");
            }
            var category = _mapper.Map<Category>(categoryDTO);
            await _categoryRepository.Add(category);
            return await Task.FromResult(true);
        }

        public async Task<CategoryListViewModel> GetAllByPageNumber(int page, int limit)
        {
            var categoryList = await _categoryRepository.GetAllByPageNumber(page, limit);
            return categoryList;
        }
        public async Task<IEnumerable<CategoryViewModel>> GetAll()
        {
            try
            {
                var data = await _categoryRepository.GetAllAsync();
                var categoryList = _mapper.Map<ICollection<CategoryViewModel>>(data);
                return categoryList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<bool> Update(Guid catId, CategoryDTO categoryDTO)
        {

            await _categoryRepository.Update(catId, categoryDTO);
            return await Task.FromResult(true);
        }

        public async Task<bool> Delete(Guid catId)
        {
            var category = await _categoryRepository.GetById(catId) ??
                throw new KeyNotFoundException(Messages.CategoryNotFound);
            category!.IsDelete = true;
            await _categoryRepository.Update(category);
            return await Task.FromResult(true);
        }

        public async Task<CategoryViewModel> GetById(Guid catId)
        {
            var category = await _categoryRepository.GetById(catId) ??
                throw new KeyNotFoundException(Messages.CategoryNotFound);
            var categoryVM = _mapper.Map<CategoryViewModel>(category);
            return categoryVM;
        }
    }
}
