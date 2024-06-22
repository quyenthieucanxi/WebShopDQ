using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.DTO;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Services.IServices
{
    public interface IUserService
    {
        Task<UserInfoViewModel> GetById(Guid userId);
        Task<bool> Update(Guid userId, UserInfoDTO model);
        Task<UserListViewModel> GetAll();
        Task<bool> Delete(Guid id);
        Task<bool> CheckUserByEmail(string email);
        Task<bool> AddLikePost(Guid userId, Guid postId);
        Task<PostListViewModel> GetSavesPost(Guid userId);
        Task<bool> RemoveSavesPost(Guid userId, Guid postId);
        Task<bool> CheckSavesPost(Guid userId, string pathPost);
        Task<bool> CreateAddRessShipping(Guid userId, AddressShippingDTO addressShippingDTO);
        Task<AddressShippngListViewModel> GetAddressShopping(Guid userId);
        Task<bool> RemoveAddressShopping(Guid userId, Guid addressShippingId);
        Task<bool> UpdateAddressShopping(Guid userId, Guid addressShippingId, AddressShippingDTO addressShippingDTO);
        Task<AddressShippngViewModel> GetAddressShoppingDeFault(Guid userId);
        Task<bool> SetAddressShopping(Guid userId,Guid addressShippingId);
        Task<bool> CreateShop(Guid userId, ShopDTO shopDTO);
        Task<UserInfoViewModel> GetProfile(string url);
        Task<bool> UpdateAvatar(Guid userId, string urlAvt);
    }
}
