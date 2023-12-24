﻿using Microsoft.Extensions.DependencyInjection;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.Repositories;
using WebShopDQ.App.Services.IServices;
using WebShopDQ.App.Data;

namespace WebShopDQ.App.Dependency
{
    public static class DependencyInjectionRepository
    {
        public static IServiceCollection AddRepository(this IServiceCollection service) 
        {
            service.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
            service.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            service.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
            service.AddTransient<IGetHTMLBodyRepository, GetHTMLBodyRepository>();
            service.AddTransient<IEmailRepository, EmailRepository>();
            service.AddTransient<IDecodeRepository, DecodeRepository>();
            service.AddTransient<IUserRepository, UserRepository>();
            service.AddTransient<IPostRepository, PostRepository>();
            service.AddTransient<ICategoryRepository, CategoryRepository>();
            service.AddTransient<IFriendshipRepository, FriendshipRepository>();
            service.AddTransient<IOrderRepository, OrderRepository>();
            service.AddTransient<IFileRepository, FileRepository>();
            service.AddTransient<ISavePostRepository, SavePostRepository>();
            service.AddTransient<IAddressShippingRepository, AddressShippingRepository>();
            service.AddTransient<IShopRepository, ShopRepository>();
            return service;
        }
    }
}
