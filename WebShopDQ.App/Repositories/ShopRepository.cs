using AutoMapper;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Common.Constant;
using WebShopDQ.App.Common.Exceptions;
using WebShopDQ.App.Data;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.ViewModels.Authentication;

namespace WebShopDQ.App.Repositories
{
    public class ShopRepository : Repository<Shop>, IShopRepository
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly DatabaseContext _databaseContext;
        private readonly UserManager<User> _userManager;

        public ShopRepository(IMapper mapper, IUnitOfWork uow, UserManager<User> userManager, DatabaseContext databaseContext) : base(databaseContext)
        {
            _mapper = mapper;
            _uow = uow;
            _userManager = userManager;
            _databaseContext = databaseContext;
        }

        public async Task<bool> Create(User user, ShopDTO shopDTO)
        {
            var dataExist = await FindAsync(p => p.Path == shopDTO.Path);
            if (dataExist != null)
            {
                throw new DuplicateException("Name Shop Exist");
            }
            try
            {
                _uow.BeginTransaction();
                await _userManager.AddToRoleAsync(user, RoleConstant.Seller);
                var shop = _mapper.Map<Shop>(shopDTO);
                shop.UserId = user.Id;
                await Add(shop);
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
