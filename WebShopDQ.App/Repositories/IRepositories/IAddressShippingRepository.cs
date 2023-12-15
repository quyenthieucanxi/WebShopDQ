using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Repositories.IRepositories
{
    public interface IAddressShippingRepository : IRepository<AddressShipping>
    {
        Task<bool> CreateAddress(Guid UserId, AddressShippingDTO addressShippingDTO);
        Task<bool> UpdateAddress(Guid userId,Guid addressId, AddressShippingDTO addressShippingDTO);
        Task<AddressShipping?> GetDefault(Guid UserId);
        Task<bool> SetDefault(Guid userId,Guid addressShipping);
    }
}
