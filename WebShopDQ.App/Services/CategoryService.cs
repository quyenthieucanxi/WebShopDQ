using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Common;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Migrations;
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
            try
            {
                var category = new Category
                {
                    CategoryName = categoryDTO.CategoryName
                };
                await _categoryRepository.Add(category);
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CategoryListViewModel> GetAllByPageNumber(int page, int limit)
        {
            var categoryList = await _categoryRepository.GetAllByPageNumber(page, limit);
            return categoryList;
        }
        public async Task<IEnumerable<CategoryViewModel>> GetAll()
        {
            var categoryList = new List<CategoryViewModel>();
            try
            {
                var data = await _categoryRepository.GetAllAsync() ;
                foreach (var item in data)
                {
                    var cat = _mapper.Map<CategoryViewModel>(item);
                    categoryList.Add(cat);
                }

                return categoryList ;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<bool> Update(Guid idCategory, CategoryDTO categoryDTO)
        {
            var category = await _categoryRepository.GetById(idCategory) ??
                throw new KeyNotFoundException(Messages.CategoryNotFound);
            category!.CategoryName = categoryDTO.CategoryName;
            await _categoryRepository.Update(category);
            return await Task.FromResult(true);
        }

        public async Task<bool> Delete(Guid idCategory)
        {
            var category = await _categoryRepository.GetById(idCategory);
            if (category != null)
            {
                category!.IsDelete = true;
                await _categoryRepository.Update(category);
                return await Task.FromResult(true);
            }
            throw new KeyNotFoundException(Messages.CategoryNotFound);
        }

        
    }
}
