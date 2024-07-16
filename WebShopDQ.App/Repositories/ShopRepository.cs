using AutoMapper;
using CloudinaryDotNet.Actions;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Common;
using WebShopDQ.App.Common.Constant;
using WebShopDQ.App.Common.Exceptions;
using WebShopDQ.App.Data;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.Services;
using WebShopDQ.App.ViewModels.Authentication;

namespace WebShopDQ.App.Repositories
{
    public class ShopRepository : Repository<Shop>, IShopRepository
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly DatabaseContext _databaseContext;
        private readonly UserManager<User> _userManager;
        private IBackgroundJobClient _backgroundJobClient;

        public ShopRepository(IMapper mapper, 
            IUnitOfWork uow, 
            UserManager<User> userManager,
            DatabaseContext databaseContext,
            IBackgroundJobClient backgroundJobClient) : base(databaseContext)
        {
            _mapper = mapper;
            _uow = uow;
            _userManager = userManager;
            _databaseContext = databaseContext;
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task<bool> Create(User user, ShopDTO shopDTO)
        {
            var dataExist = await FindAsync(p => p.Path == shopDTO.Path);
            if (dataExist != null)
            {
                throw new DuplicateException("Tên Shop đã tồn tại");
            }
            try
            {
                var shop = _mapper.Map<Shop>(shopDTO);
                shop.UserId = user.Id;
                shop.Status = ShopStatus.Pending;
                await Add(shop);
                _backgroundJobClient.Enqueue<NotifyService>(service => service.NotifyWhenUserCreateShop(user.Id,user.FullName,user.AvatarUrl,shop.Name));
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<bool> Update(Guid idShop, string status, Guid userId)
        {
            var shopExist = await GetById(idShop)
                ?? throw new KeyNotFoundException(Messages.ShopNotFound);
            try
            {
                _uow.BeginTransaction();
                var user = await _userManager.FindByIdAsync(shopExist.UserId.ToString());
                shopExist.Status = status;
                await Update(shopExist);
                if (status == ShopStatus.View)
                {
                    await _userManager.AddToRoleAsync(user, RoleConstant.Seller);
                }
                await _uow.SaveChanges();
                _uow.CommitTransaction();
                _backgroundJobClient.Enqueue<NotifyService>(service => service.NotifyWhenUpdateStatusShop(userId, user.Id,user.AvatarUrl, status,shopExist.Name));
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
