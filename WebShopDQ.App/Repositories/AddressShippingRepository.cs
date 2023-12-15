using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Common;
using WebShopDQ.App.Data;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Repositories
{
    public class AddressShippingRepository : Repository<AddressShipping>, IAddressShippingRepository
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly DatabaseContext _databaseContext;
        public AddressShippingRepository(DatabaseContext databaseContext, IUnitOfWork uow, IMapper mapper) : base(databaseContext)
        {   
            _uow = uow;
            _mapper = mapper;
            _databaseContext = databaseContext;
        }

        public async Task<bool> CreateAddress(Guid UserId, AddressShippingDTO addressShippingDTO)
        {
            try
            {
                _uow.BeginTransaction();
                var addressDefaultExist = await GetDefault(UserId);
                if (addressShippingDTO.IsDefault && addressDefaultExist != null)
                {              
                    addressDefaultExist!.IsDefault = false;
                    await Update(addressDefaultExist);
                }
                var addRessShipping = _mapper.Map<AddressShipping>(addressShippingDTO);
                addRessShipping.UserId = UserId;
                await Add(addRessShipping);
                _uow.SaveChanges();
                _uow.CommitTransaction();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _uow.RollbackTransaction();
                throw new Exception(ex.Message);
            }
        }

        public async Task<AddressShipping?> GetDefault(Guid UserId)
        {
            var address = await FindAsync(p => p.UserId == UserId && p.IsDefault);
            return address;
        }

        public async Task<bool> SetDefault(Guid userId, Guid addressShippingId)
        {
            try
            {
                _uow.BeginTransaction();
                var addressDefault = await GetDefault(userId);
                if (addressDefault != null)
                {
                    addressDefault.IsDefault = false;
                    await Update(addressDefault);
                }
                var addressExist = await GetById(addressShippingId) ?? throw new KeyNotFoundException(Messages.AddressShippingNotFound);
                addressExist.IsDefault = true;
                await Update(addressExist);
                _uow.SaveChanges();
                _uow.CommitTransaction();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _uow.RollbackTransaction();
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAddress(Guid userId, Guid addressId, AddressShippingDTO addressShippingDTO)
        {
            try
            {
                _uow.BeginTransaction();
                var addressExist = await GetById(addressId) ?? throw new KeyNotFoundException(Messages.AddressShippingNotFound);
                if (addressShippingDTO.IsDefault)
                {
                    var addressDefaultExist = await GetDefault(userId);
                    addressDefaultExist!.IsDefault = false;
                    await Update(addressDefaultExist);
                }
                var entry = _databaseContext.Entry(addressExist);
                entry.CurrentValues.SetValues(addressShippingDTO);
                _uow.SaveChanges();
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
